namespace SampleConsole.compiler.preprocess
{
    using Microsoft.Dnx.Compilation.CSharp;

    public class DiscoverModulesCompileModule : Fx.ModulesCompileModule
    {
        public override void BeforeCompile(BeforeCompileContext context)
        {
            System.Diagnostics.Debugger.Launch();

            base.BeforeCompile(context);
        }
    }
}