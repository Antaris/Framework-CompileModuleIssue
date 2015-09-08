// <copyright company="Fresh Egg Limited" file="ConnectionStringFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;
    using Microsoft.Dnx.Runtime;
    using Microsoft.Framework.Configuration;


    /// <summary>
    /// Provides services for creating connection strings.
    /// </summary>
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IModule _hostModule;
        private readonly IApplicationEnvironment _applicationEnvironment;
        private readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();

        const string ConfigurationSection = "Data";
        const string ServerKey = "Server";
        const string DatabaseKey = "Database";
        const string UserKey = "User";
        const string PasswordKey = "Password";
        const string VersionedKey = "Versioned";
        const string IntegratedSecurityKey = "IntegratedSecurity";
        const string CustomKey = "Custom";

        /// <summary>
        /// Initialises a new instance of <see cref="ConnectionStringFactory"/>
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConnectionStringFactory(IConfiguration configuration, IModule hostModule, IApplicationEnvironment applicationEnvironment)
        {
            _configuration = Ensure.ArgumentNotNull(configuration, nameof(configuration));
            _hostModule = Ensure.ArgumentNotNull(hostModule, nameof(hostModule));
            _applicationEnvironment = Ensure.ArgumentNotNull(applicationEnvironment, nameof(applicationEnvironment));
        }

        /// <inheritdoc />
        public string CreateConnectionString(string name, string fallback = null)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));

            return _cache.GetOrAdd(name, k => CreateConnectionStringCore(name, fallback));
        }

        /// <summary>
        /// Creates a connection string for the given name.
        /// </summary>
        /// <param name="name">The connection string name.</param>
        /// <param name="fallback">The fallback connection string name.</param>
        /// <returns>The connection string.</returns>
        private string CreateConnectionStringCore(string name, string fallback)
        {
            var config = _configuration.GetSection(ConfigurationSection);

            if (HasValue(config, name, ServerKey))
            {
                return Update(BuildConnectionString(config, name));
            }
            else if (HasValue(config, name))
            {
                return Update(config[name]);
            }

            if (!string.IsNullOrWhiteSpace(fallback) && !string.Equals(fallback, name, StringComparison.OrdinalIgnoreCase))
            {
                return CreateConnectionString(fallback, null);
            }

            return null;
        }

        /// <summary>
        /// Builds a connection string from a set of nested keys.
        /// </summary>
        /// <param name="name">The connection string name.</param>
        /// <returns>The connection string.</returns>
        private string BuildConnectionString(IConfiguration configuration, string name)
        {
            bool integrated = GetValue<bool>(configuration, name, IntegratedSecurityKey);
            bool versioned = GetValue<bool>(configuration, name, VersionedKey);

            string server = GetValue<string>(configuration, name, ServerKey);
            string database = GetValue<string>(configuration, name, DatabaseKey);
            string user = GetValue<string>(configuration, name, UserKey);
            string password = GetValue<string>(configuration, name, PasswordKey);

            string custom = GetValue<string>(configuration, name, CustomKey);

            var builder = new StringBuilder();
            builder.AppendFormat("Server={0};", server);
            builder.AppendFormat("Database={0}{1};", database, versioned ? $"v{_hostModule.Version.Major}.{_hostModule.Version.Minor}" : "");
            
            if (integrated)
            {
                builder.AppendFormat("Integrated Security=true;");
            }
            else
            {
                builder.AppendFormat("User={0}", user);
                builder.AppendFormat("Password={0}", password);
            }

            builder.AppendFormat("MultipleActiveResultSets=true;");
           
            if (!string.IsNullOrWhiteSpace(custom))
            {
                builder.Append(custom);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets a value from a configuration subkey.
        /// </summary>
        /// <typeparam name="T">The configuration value type.</typeparam>
        /// <param name="configuration">The configuration instance.</param>
        /// <param name="parts">The key parts.</param>
        /// <returns>The configuration value.</returns>
        private T GetValue<T>(IConfiguration configuration, params string[] parts)
        {
            string path = string.Join(":", parts);
            string value = configuration[path];
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)value;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Determines if the given subkey exists.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        /// <param name="parts">The key parts.</param>
        /// <returns>The configuration value.</returns>
        private bool HasValue(IConfiguration configuration, params string[] parts)
        {
            string path = string.Join(":", parts);
            return !string.IsNullOrWhiteSpace(configuration[path]);
        }

        /// <summary>
        /// Updates the connection string to handle environment strings.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The updated connection string.</returns>
        private string Update(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return connectionString;
            }

            return connectionString.Replace("|DataDirectory|", Path.Combine(_applicationEnvironment.ApplicationBasePath, "data"));
        }
    }
}