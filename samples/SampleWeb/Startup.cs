namespace SampleWeb
{
    using System;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Data.Entity;
    using Microsoft.Dnx.Runtime;
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;
    using Fx;
    using Fx.Security;

    /// <summary>
    /// The application startup class
    /// </summary>
    public class Startup
    {
        public Host<HostModule> Host;

        /// <summary>
        /// Configures the services required by the website.
        /// </summary>
        /// <param name="services">The service collection provided by the website</param>
        /// <returns>The new service provider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Create the host.
            Host = new Host<HostModule>(new ModuleProvider(), CreateConfiguration(services.BuildServiceProvider()));

            Host.ConfigureEntityFramework(
                // Add SQL Server support to the website.
                b => b.AddSqlServer(), 
                // Configure the DB contexts to use SQL server.
                (cf, b) => b.UseSqlServer(cf.CreateConnectionString("SampleWeb", "Default"))
            );

            // Configure the available services.
            return Host.ConfigureServices(services);
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The ASP.NET application builder.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                var clock = context.RequestServices.GetRequiredService<IClock>();
                var userService = context.RequestServices.GetRequiredService<IUserService>();
                var user = userService.Get(1);

                await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync($"The current time is: {clock.UtcNow}");
            });
        }

        /// <summary>
        /// Creates a configuration for the current environment.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>The configuration.</returns>
        private IConfiguration CreateConfiguration(IServiceProvider serviceProvider)
        {
            var applicationEnvironment = serviceProvider.GetRequiredService<IApplicationEnvironment>();
            var builder = new ConfigurationBuilder(applicationEnvironment.ApplicationBasePath)
                .AddJsonFile(@".\config\databases.json")
                .AddEnvironmentVariables("Fx");

            return builder.Build();
        }
    }
}
