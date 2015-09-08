// <copyright company="Fresh Egg Limited" file="BuildMigrationsCompileModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.compiler.preprocess
{
    using Microsoft.Dnx.Compilation.CSharp;

    /// <summary>
    /// Discovers and generates migration classes at compile time.
    /// </summary>
    public class BuildMigrationsCompileModule : Fx.Data.MigrationsCompileModule
    {
        public override void BeforeCompile(BeforeCompileContext context)
        {
            base.BeforeCompile(context);
        }
    }
}