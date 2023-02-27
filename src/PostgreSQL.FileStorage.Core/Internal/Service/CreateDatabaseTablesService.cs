using Dapper;
using Npgsql;
using PostgreSQL.FileStorage.Core.Internal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.Internal.Service
{
   
    internal class CreateDatabaseTablesService : ICreateDatabaseTablesService
    {
        private readonly string _connectionString;
        private readonly NpgsqlConnection _connection;

        public CreateDatabaseTablesService(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        public async Task<bool> SchemaExists(string schemaName)
        {
            string commandText = $"SELECT nspname FROM pg_catalog.pg_namespace where Upper(nspname) = upper(@schemaName) ORDER BY nspname Limit 1;";
            var queryArgs = new { SchemaName = schemaName };

            var result = await _connection.QueryFirstOrDefaultAsync<string>(commandText, queryArgs);
            if (result == default)
            {
                return false;
            }
            return true;
        }

        public async Task CreateSchema(string schemaName)
        {
            var schemeExists = await SchemaExists(schemaName);
            if (schemeExists == true)
            {
                throw new Exception("Schema Already Exists");
            }

            var userName = _connection.UserName;

            string commandText = $"CREATE SCHEMA IF NOT EXISTS {schemaName} AUTHORIZATION {userName};";

            await _connection.ExecuteAsync(commandText);
        }


        public async Task<bool> TableExists(string schemaName, string tableName)
        {
            string commandText = $"SELECT EXISTS (SELECT FROM information_schema.tables WHERE  upper(table_schema) = upper('{schemaName}') AND upper(table_name) = upper('{tableName}'))";

            var result = await _connection.QueryFirstOrDefaultAsync<bool>(commandText);
            if (result == default)
            {
                return false;
            }
            return result;
        }

        public async Task CreateFileStorageTableIfNotExists(string schemaName, string tableName)
        {
            var commandText = @"CREATE TABLE IF NOT EXISTS " + schemaName + "." + tableName + @" (
	                            Id UUID PRIMARY KEY,
	                            ApplicationName VARCHAR ( 256 ) NOT NULL,
                                Entity VARCHAR ( 256 ) NOT NULL,
	                            PrimaryKey VARCHAR ( 256 ) NOT NULL,
	                            Category VARCHAR ( 256 ) NULL,
                                Description VARCHAR ( 2048 ) NULL,
	                            Filename VARCHAR ( 256 ) NOT NULL,
	                            FileType VARCHAR ( 256 ) NULL,
	                            CreatedDateUtc TIMESTAMP NOT NULL,
	                            CreatedBy VARCHAR ( 256 ) NULL,
	                            CreatedByName VARCHAR ( 256 ) NULL,
	                            FileContent bytea NULL
                            );";

            await _connection.ExecuteAsync(commandText);
        }

        public async Task CreateScemaAndTableIfNotExists(string schemaName, string tableName)
        {
            var schemaExists = await SchemaExists(schemaName);
            if (schemaExists == false)
            {
                await CreateSchema(schemaName);
            }
            await CreateFileStorageTableIfNotExists(schemaName, tableName);
        }

    }
}
