namespace SampleConsole
{
    using System;
    using System.Linq;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Framework.Configuration;
    using Microsoft.Dnx.Runtime;
    using Fx;

    public class Program
    {
        private readonly IServiceProvider _baseServiceProvider;
        private readonly IApplicationEnvironment _applicationEnvironment;

        public Program(IServiceProvider baseServiceProvider, IApplicationEnvironment applicationEnvironment)
        {
            _baseServiceProvider = Ensure.ArgumentNotNull(baseServiceProvider, nameof(baseServiceProvider));
            _applicationEnvironment = Ensure.ArgumentNotNull(applicationEnvironment, nameof(applicationEnvironment));
        }

        public void Main(string[] args)
        {
            var provider = new ModuleProvider();
            var services = new ServiceCollection();

            services.AddSingleton<IApplicationEnvironment>(s => _applicationEnvironment);
            
            var config = new ConfigurationBuilder(_applicationEnvironment.ApplicationBasePath)
                .AddJsonFile(@".\databases.json")
                .Build();

            Console.WriteLine(provider.GetModules().Count());
            Console.ReadKey();
        }
    }
}