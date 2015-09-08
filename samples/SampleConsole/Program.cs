using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fx;
using Fx.Data;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;
using Microsoft.Dnx.Runtime;

namespace SampleConsole
{
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

            var host = new Host<HostModule>(provider, config);

            host.ConfigureEntityFramework(
                // Add support for the InMemory database.
                b => b.AddInMemoryDatabase(),
                // Use the InMemry database.
                (cb, b) => b.UseInMemoryDatabase()
            );

            var servicesProvider = host.ConfigureServices(services);

            var userService = servicesProvider.GetRequiredService<Fx.Security.IUserService>();

            int userId = userService.Create(new Fx.Security.User
            {
                Forename = "Matt",
                Surname = "Abbott",
                Username = "admin",
                Conventions = "admin,public"
            });

            var user = userService.Get(userId);
            Console.WriteLine($"{user.Forename} {user.Surname}");
        }
    }
}