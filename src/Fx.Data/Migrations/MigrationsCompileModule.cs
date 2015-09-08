// <copyright company="Fresh Egg Limited" file="MigrationsCompileModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.Dnx.Compilation.CSharp;

    using T = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree;
    using F = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
    using K = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

    /// <summary>
    /// Performs compile-time generation of migrations classes.
    /// </summary>
    public abstract class MigrationsCompileModule : ICompileModule
    {
        private static readonly FieldInfo ResourceNameFieldInfo = typeof(ResourceDescription).GetField("ResourceName", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Regex VersionFilenameRegex = new Regex(@"v(\d+\._\d+\._\d+)\.(.*?)\.sql", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        /// <inheritdoc />
        public virtual void AfterCompile(AfterCompileContext context) { }

        /// <inheritdoc />
        public virtual void BeforeCompile(BeforeCompileContext context)
        {
            var migrations = GetMigrations(context.Resources);
            var invalid = migrations.Where(m => m.Version == null).ToList();
            var valid = migrations.Except(invalid).ToList();

            if (invalid.Any())
            {
                // We have errors, so add a diagnostic error.
                foreach (var migration in invalid)
                {
                    var diagnostic = Diagnostic.Create(
                        "FX-DATA0001",
                        "Data",
                        new SimpleString($"Unable to bundle {migration.FileName} as we couldn't determine the module version. Is the file in a nested folder, e.g. /compiler/preprocess/data/v1.0.0/{migration.FileName}?"),
                        DiagnosticSeverity.Error,
                        DiagnosticSeverity.Error,
                        false,
                        0
                    );

                    context.Diagnostics.Add(diagnostic);
                }
            }
            else if (valid.Any())
            {
                var groupings = valid
                    .GroupBy(a => a.Version)
                    .ToList();

                var trees = new List<SyntaxTree>();

                foreach (var grouping in groupings)
                {
                    var statements = grouping.Select(p => F.ParseStatement($"await ExecuteScriptAsync(\"{p.ResourceName}\");"));

                    var cu = F.CompilationUnit()
                        .AddUsings(F.UsingDirective(F.QualifiedName(F.IdentifierName("Fx"), F.IdentifierName("Data"))))
                        .AddMembers(
                            F.NamespaceDeclaration(F.IdentifierName(context.Compilation.AssemblyName))
                                .AddMembers(
                                    F.NamespaceDeclaration(F.IdentifierName("Migrations"))
                                        .AddMembers(
                                            F.ClassDeclaration($"MigrationToV{grouping.Key.Replace(".", "_")}")
                                                .WithModifiers(F.TokenList(F.Token(K.PublicKeyword)))
                                                .WithAttributeLists(
                                                    F.SingletonList(
                                                        F.AttributeList(
                                                            F.SingletonSeparatedList(
                                                                F.Attribute(F.IdentifierName("VersionedMigrationAttribute"))
                                                                    .WithArgumentList(
                                                                        F.AttributeArgumentList(
                                                                            F.SingletonSeparatedList(
                                                                                F.AttributeArgument(
                                                                                    F.LiteralExpression(
                                                                                        K.StringLiteralExpression,
                                                                                        F.Literal(
                                                                                            F.TriviaList(),
                                                                                            $"{grouping.Key}",
                                                                                            $"{grouping.Key}",
                                                                                            F.TriviaList()
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                            )
                                                        )
                                                    )
                                                )
                                                .WithBaseList(
                                                    F.BaseList(
                                                        F.SingletonSeparatedList<BaseTypeSyntax>(
                                                            F.SimpleBaseType(F.IdentifierName("Migration"))
                                                        )
                                                    )
                                                )
                                                .AddMembers(
                                                    F.MethodDeclaration(
                                                        F.PredefinedType(F.Token(K.VoidKeyword)),
                                                        F.Identifier("Execute")
                                                    )
                                                    .WithModifiers(F.TokenList(F.Token(K.PublicKeyword), F.Token(K.OverrideKeyword), F.Token(K.AsyncKeyword)))
                                                    .WithBody(
                                                        F.Block(
                                                            F.Token(K.OpenBraceToken),
                                                            F.List(statements),
                                                            F.Token(K.CloseBraceToken)
                                                        )
                                                    )
                                                )
                                        )
                                )
                        )
                        .NormalizeWhitespace();

                    trees.Add(T.Create(cu));
                }

                context.Compilation = context.Compilation.AddSyntaxTrees(trees);
            }
            else
            {
                var diagnostic = Diagnostic.Create(
                    "FX-DATA0002",
                    "Data",
                    new SimpleString($"There are no migrations to add."),
                    DiagnosticSeverity.Info,
                    DiagnosticSeverity.Info,
                    false,
                    0
                );

                context.Diagnostics.Add(diagnostic);
            }
        }

        /// <summary>
        /// Gets the available migrations found in the module.
        /// </summary>
        /// <param name="resources">The set of resources.</param>
        /// <returns>The set of migrations.</returns>
        private IEnumerable<Migration> GetMigrations(IEnumerable<ResourceDescription> resources)
        {
            return resources
                .Select(r => (string)ResourceNameFieldInfo.GetValue(r))
                .Where(n => n.ToLower().Contains("compiler.resources.data.v"))
                .Where(n => n.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                .Select(n => new Migration
                {
                    ResourceName = n,
                    Version = GetVersion(n),
                    FileName = Path.GetFileName(n)
                });
        }

        /// <summary>
        /// Gets the version from the resource name.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <returns>The version.</returns>
        private string GetVersion(string resourceName)
        {
            const string match = "compiler.resources.data.v";
            int start = resourceName.IndexOf(match) + match.Length;
            resourceName = resourceName.Substring(start);
            start = resourceName.IndexOf('.'); // Minor
            start = resourceName.IndexOf('.', start + 1); // Patch
            start = resourceName.IndexOf('.', start + 1); // Folder

            return resourceName.Substring(0, start).Replace("_", "");
        }

        /// <summary>
        /// Represents a migration script.
        /// </summary>
        private class Migration
        {
            /// <summary>
            /// Gets or sets the full resource name.
            /// </summary>
            public string ResourceName { get; set; }

            /// <summary>
            /// Gets or sets the version.
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// Gets or sets the resource filename.
            /// </summary>
            public string FileName { get; set; }
        }

        /// <summary>
        /// Represents a simple localisable string.
        /// </summary>
        private class SimpleString : LocalizableString
        {
            private readonly string _value;

            /// <summary>
            /// Initialises a new instance of <see cref="SimpleString"/>
            /// </summary>
            /// <param name="value">The string value.</param>
            public SimpleString(string value)
            {
                _value = value;
            }

            /// <inheritdoc />
            protected override string GetText(IFormatProvider formatProvider)
            {
                return _value;
            }

            /// <inheritdoc />
            protected override int GetHash()
            {
                return _value.GetHashCode();
            }

            /// <inheritdoc />
            protected override bool AreEqual(object other)
            {
                return false;
            }
        }
    }
}