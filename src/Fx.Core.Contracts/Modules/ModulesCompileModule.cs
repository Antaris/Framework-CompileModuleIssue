// <copyright company="Fresh Egg Limited" file="ModulesCompileModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.Dnx.Compilation.CSharp;

    using T = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree;
    using F = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
    using K = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

    /// <summary>
    /// Discovers all modules in a solution and generates module provider entries.
    /// </summary>
    public abstract class ModulesCompileModule : ICompileModule
    {
        public static MethodInfo GetMetadataMethodInfo = typeof(PortableExecutableReference).GetMethod("GetMetadata", BindingFlags.NonPublic | BindingFlags.Instance);
        public static FieldInfo CachedSymbolsFieldInfo = typeof(AssemblyMetadata).GetField("CachedSymbols", BindingFlags.NonPublic | BindingFlags.Instance);
        private ConcurrentDictionary<MetadataReference, string[]> _cache = new ConcurrentDictionary<MetadataReference, string[]>();

        /// <inheritdoc />
        public virtual void AfterCompile(AfterCompileContext context)
        {
        }

        /// <inheritdoc />
        public virtual void BeforeCompile(BeforeCompileContext context)
        {
            string ns = GetModuleProviderNamespace(context.Compilation.SyntaxTrees.ToArray());

            var modules = GetAvailableModules(context.Compilation).ToList();
            var statements = modules.Select(m => SyntaxFactory.ParseStatement("AddModule<" + m + ">();")).ToList();

            var cu = F.CompilationUnit()
                .AddMembers(
                    F.NamespaceDeclaration(F.IdentifierName(ns))
                        .AddMembers(
                            F.ClassDeclaration("ModuleProvider")
                                .WithModifiers(F.TokenList(F.Token(K.PartialKeyword)))
                                .AddMembers(
                                    F.MethodDeclaration(F.PredefinedType(F.Token(K.VoidKeyword)), "Setup")
                                        .WithModifiers(F.TokenList(F.Token(K.ProtectedKeyword), F.Token(K.OverrideKeyword)))
                                        .WithBody(F.Block(statements))
                                )
                        )
                )
                .NormalizeWhitespace(indentation: "\t");

            var tree = T.Create(cu);
            context.Compilation = context.Compilation.AddSyntaxTrees(tree);
        }

        /// <summary>
        /// Gets the namespace of the module provider instance within the compilation.
        /// </summary>
        /// <param name="trees">The set of syntax trees that form the compilation.</param>
        /// <returns>The namespace, if found, otherwise null.</returns>
        private string GetModuleProviderNamespace(SyntaxTree[] trees)
        {
            var provider = trees.SelectMany(t => t.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>())
                .Where(c => c.Identifier.ValueText.Equals("ModuleProvider"))
                .FirstOrDefault();

            if (provider == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            var ns = provider.Parent as NamespaceDeclarationSyntax;
            while (ns != null)
            {
                if (builder.Length > 0)
                {
                    builder.Insert(0, ".");
                }
                builder.Insert(0, ns.Name.ToString());
                ns = ns.Parent as NamespaceDeclarationSyntax;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets all of the available modules found in the given compilation.
        /// </summary>
        /// <param name="compilation">The compilation.</param>
        /// <returns>The set of modules, as fully-qualified names.</returns>
        private IEnumerable<string> GetAvailableModules(Compilation compilation)
        {
            var list = new List<string>();
            string[] modules = null;

            // Get the set of references for the compilation.
            var refs = compilation.References.ToList();

            // Get the assembly references.
            var assemblies = refs.OfType<PortableExecutableReference>().ToList();
            foreach (var assemblyRef in assemblies)
            {
                if (!_cache.TryGetValue(assemblyRef, out modules))
                {
                    modules = GetAssemblyModules(assemblyRef);
                    _cache.AddOrUpdate(assemblyRef, modules, (k, v) => modules);
                    list.AddRange(modules);
                }
                else
                {
                    // We've already included this assembly.
                }
            }

            // Next deal with the each referenced compilation.
            var compilations = refs.OfType<CompilationReference>().ToList();

            foreach (var compilationRef in compilations)
            {
                if (!_cache.TryGetValue(compilationRef, out modules))
                {
                    modules = GetAvailableModules(compilationRef.Compilation).ToArray();
                    _cache.AddOrUpdate(compilationRef, modules, (k, v) => modules);
                    list.AddRange(modules);
                }
            }

            // Finally deal with types defined in our own assembly.
            list.AddRange(GetModuleClassDeclarations(compilation));

            return list;
        }

        /// <summary>
        /// Gets all the available modules found in the given assembly reference.
        /// </summary>
        /// <param name="reference">The assembly reference.</param>
        /// <returns>The set of modules, as fully-qualified names.</returns>
        private string[] GetAssemblyModules(PortableExecutableReference reference)
        {
            var metadata = GetMetadataMethodInfo.Invoke(reference, null) as AssemblyMetadata;
            if (metadata != null)
            {
                var assemblySymbol = ((IEnumerable<IAssemblySymbol>)CachedSymbolsFieldInfo.GetValue(metadata)).First();
                if (assemblySymbol.Name.StartsWith("Fx"))
                {
                    var types = GetTypeSymbols(assemblySymbol.GlobalNamespace).Where(t => Filter(t));
                    return types.Select(t => GetFullMetadataName(t)).ToArray();
                }
            }

            return new string[0];
        }

        /// <summary>
        /// Filters the given type symbol to ensure it meets the criteria for representing a module.
        /// </summary>
        /// <param name="symbol">The type symbol.</param>
        /// <returns>True if the symbol meets the criteria, otherwise false.</returns>
        private bool Filter(ITypeSymbol symbol)
        {
            return symbol.IsReferenceType && !symbol.IsAbstract && !symbol.IsAnonymousType && symbol.AllInterfaces.Any(i => GetFullMetadataName(i) == "Fx.IModule");
        }

        /// <summary>
        /// Gets all the available type symbols form the given namespace symbol, and nested type symbols.
        /// </summary>
        /// <param name="ns">The namespace symbol.</param>
        /// <returns>The set of type symbols.</returns>
        private IEnumerable<ITypeSymbol> GetTypeSymbols(INamespaceSymbol ns)
        {
            foreach (var typeSymbol in ns.GetTypeMembers().Where(t => !t.Name.StartsWith("<")))
            {
                yield return typeSymbol;
            }

            foreach (var namespaceSymbol in ns.GetNamespaceMembers())
            {
                foreach (var typeSymbol in GetTypeSymbols(namespaceSymbol))
                {
                    yield return typeSymbol;
                }
            }
        }

        /// <summary>
        /// Gets all module class declarations in the given compilation.
        /// </summary>
        /// <param name="compilation">The target compilation.</param>
        /// <returns>The set of modules, as fully-qualified names.</returns>
        private IEnumerable<string> GetModuleClassDeclarations(Compilation compilation)
        {
            var trees = compilation.SyntaxTrees.ToList();
            var models = trees.Select(t => compilation.GetSemanticModel(t)).ToList();

            for (var i = 0; i < trees.Count; i++)
            {
                var tree = trees[i];
                var model = models[i];

                var types = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
                foreach (var type in types)
                {
                    var symbol = model.GetDeclaredSymbol(type) as ITypeSymbol;
                    if (symbol != null && Filter(symbol))
                    {
                        yield return GetFullMetadataName(symbol);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the fully-qualified name for the given symbol.
        /// </summary>
        /// <param name="symbol">The namespace or type symbol.</param>
        /// <returns>The fully-qualified name.</returns>
        private static string GetFullMetadataName(INamespaceOrTypeSymbol symbol)
        {
            ISymbol s = symbol;
            var builder = new StringBuilder(s.MetadataName);

            var last = s;
            s = s.ContainingSymbol;
            while (!IsRootNamespace(s))
            {
                builder.Insert(0, '.');
                builder.Insert(0, s.MetadataName);
                s = s.ContainingSymbol;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines if the given symbol represents the root namespace symbol.
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <returns>True if the symbol represents the root namespace symbol, otherwise false.</returns>
        private static bool IsRootNamespace(ISymbol symbol)
        {
            return symbol is INamespaceSymbol && ((INamespaceSymbol)symbol).IsGlobalNamespace;
        }
    }
}