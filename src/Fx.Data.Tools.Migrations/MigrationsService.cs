namespace Fx.Data.Tools.Migrations
{
    /// <summary>
    /// Provides services for performing database migrations.
    /// </summary>
    public class MigrationsService
    {
        private readonly IConnectionStringFactory _connectionStringFactory;

        /// <summary>
        /// Initialises a new instance of <see cref="MigrationsService"/>
        /// </summary>
        /// <param name="connectionStringFactory">The connection string factory.</param>
        public MigrationsService(IConnectionStringFactory connectionStringFactory)
        {
            _connectionStringFactory = Ensure.ArgumentNotNull(connectionStringFactory, nameof(connectionStringFactory));
        }

        /// <summary>
        /// Migrates the database described by the given connection string name.
        /// </summary>
        /// <param name="name">The connection string name.</param>
        /// <param name="fallback">[Optional] The fallback connection string name.</param>
        public void MigrateDatabase(string name, string fallback = null)
        {
            string connectionString = _connectionStringFactory.CreateConnectionString(name, fallback);
        }
    }
}