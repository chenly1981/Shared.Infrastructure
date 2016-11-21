using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Infrastructure.Configuration.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Shared.Infrastructure;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Shared.Infrastructure.Test
{
    [TestClass]
    public class DatabaseConfigurationProviderUnitTest : UnitTestBase
    {
        [TestMethod]
        public void LoadDataShouldWork()
        {
            var options = new DatabaseConfigurationOptions
            {
                ConnectionResolver = GetOpenedConnection,
                KeyColumn = "key",
                ValueColumn = "value",
                Table = $"config_{DateTime.Now.ToString("yyyyMMddHHmmss")}"
            };

            string key = Guid.NewGuid().ToString();
            string value = Guid.NewGuid().ToString();

            InitConfigurationTable(options, key, value);

            var provider = new DatabaseConfigurationProvider(options);
            provider.Load();

            string configurationValue;
            Assert.IsTrue(provider.TryGet(key, out configurationValue));
            Assert.AreEqual(configurationValue, value);
        }

        [TestMethod]
        public void IntergrateWithConfigurationShouldWork()
        {
            var options = new DatabaseConfigurationOptions
            {
                ConnectionResolver = GetOpenedConnection,
                KeyColumn = "key",
                ValueColumn = "value",
                Table = $"config_{DateTime.Now.ToString("yyyyMMddHHmmss")}"
            };

            string key = Guid.NewGuid().ToString();
            string value = Guid.NewGuid().ToString();

            InitConfigurationTable(options, key, value);

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("notExist.json", true)
                .AddDatabase(options);

            IConfigurationRoot configuration = configurationBuilder.Build();
            
            string configurationValue = configuration[key];
            Assert.AreEqual(configurationValue, value);
        }

        private SqliteConnection GetOpenedConnection()
        {
            string connectionString = "Data Source=test.db;";

            var conn = new SqliteConnection(connectionString);
            conn.Open();

            return conn;
        }

        private void InitConfigurationTable(DatabaseConfigurationOptions options, string key, string value)
        {
            using (SqliteConnection conn = GetOpenedConnection())
            {
                string sql = string.Format($@"
create table if not exists [{options.Table}]
(
    [{options.KeyColumn}] nvarchar(100) not null primary key, 
    [{options.ValueColumn}] nvarchar(100)
);

delete from [{options.Table}];

insert into [{options.Table}]({options.KeyColumn}, {options.ValueColumn}) values ('{key}', '{value}');
");

                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }                
            }
        }
    }
}
