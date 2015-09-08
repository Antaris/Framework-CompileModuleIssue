namespace Fx.Data.Tools.Migrations
{
    using System;
    using Microsoft.Dnx.Runtime;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Framework.Configuration;

    /// <summary>
    /// Performs database migrations.
    /// </summary>
    public class Program
    {
        private readonly IServiceProvider _baseServiceProvider;
        private readonly IApplicationEnvironment _applicationEnvironment;

        /// <summary>
        /// Initialises a new instance of <see cref="Program"/>
        /// </summary>
        /// <param name="applicationEnvironment">The application environment.</param>
        public Program(IServiceProvider baseServiceProvider, IApplicationEnvironment applicationEnvironment)
        {
            _baseServiceProvider = Ensure.ArgumentNotNull(baseServiceProvider, nameof(baseServiceProvider));
            _applicationEnvironment = Ensure.ArgumentNotNull(applicationEnvironment, nameof(applicationEnvironment));
        }

        /// <summary>
        /// Entry point for database migrations tool.
        /// </summary>
        /// <param name="args">Incoming command-line arguments.</param>
        public void Main(string[] args)
        {
            // Build our configuration.
            var config = new ConfigurationBuilder(_applicationEnvironment.ApplicationBasePath)
                .AddJsonFile(@".\config\databases.json")
                .AddEnvironmentVariables("Fx")
                .AddCommandLine(args)
                .Build();

            // Add our required services.
            var services = new ServiceCollection();
            services.AddSingleton<IApplicationEnvironment>(s => _applicationEnvironment);
            services.AddSingleton<IModule>(s => new DataMigrationsModule());
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();
            services.AddSingleton<IConfiguration>(s => config);
            services.AddSingleton<MigrationsService>();
            // Create our custom service provider.
            var servicesProvider = services.BuildServiceProvider();
            // Get the migrations service.
            var migrations = servicesProvider.GetRequiredService<MigrationsService>();

            migrations.MigrateDatabase(config["target"]);
        }
    }
}