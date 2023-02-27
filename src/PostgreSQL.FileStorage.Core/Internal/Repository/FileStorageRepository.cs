using Dapper;
using Npgsql;
using PostgreSQL.FileStorage.Core.Internal.Interface;
using PostgreSQL.FileStorage.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace PostgreSQL.FileStorage.Core.Internal.Repository
{
    internal class FileStorageRepository : IFileStorageRepository
    {
        private readonly string _connectionString;
        private readonly string _schema;
        private readonly string _tableName;
        private readonly string _applicationName;
        private readonly NpgsqlConnection _connection;

        public FileStorageRepository(string connectionString, string schema, string tableName, string applicationName)
        {
            _connectionString = connectionString;
            _schema = schema;
            _tableName = tableName;
            _applicationName = applicationName;
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }



        public async Task<FileContentBasicModel> GetFileInformationById(Guid id)
        {
            var command = $"SELECT Id, Entity, PrimaryKey, Category,Description, Filename,FileType, CreatedDateUtc, CreatedBy, CreatedByName  FROM {_schema}.{_tableName} WHERE Id = @id";

            var queryArguments = new
            {
                id = id
            };

            return await _connection.QueryFirstOrDefaultAsync<FileContentBasicModel>(command, queryArguments);
        }

        public async Task<IEnumerable<FileContentBasicModel>> GetFileInformationByEntityAndPrimaryKey(string entity, string primaryKey)
        {
            var command = $"SELECT Id, Entity, PrimaryKey, Category,Description, Filename,FileType, CreatedDateUtc, CreatedBy, CreatedByName  FROM {_schema}.{_tableName} WHERE Upper(ApplicationName) = UPPER(@applicationName) and UPPER(Entity) = UPPER(@entity) and UPPER(PrimaryKey) = UPPER(@primaryKey)";

            var queryArguments = new
            {
                applicationName = _applicationName,
                entity = entity,
                primaryKey = primaryKey
            };

            return await _connection.QueryAsync<FileContentBasicModel>(command, queryArguments);
        }

        public async Task<FileContentModel> GetFileContentsById(Guid id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<FileContentModel>($"SELECT Filename, FileContent FROM {_schema}.{_tableName} WHERE Id='{id}' LIMIT 1", cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            string commandText = $"Delete {_schema}.{_tableName} where Id = @id";

            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public async Task<Guid> Create(string entity, string primaryKey, string filename, string fileType, byte[] fileContent, string? description, string? category, string? createdBy, string? createdByName, CancellationToken cancellationToken)
        {
            var idPrimarykey = Guid.NewGuid();

            string commandText = $"INSERT INTO {_schema}.{_tableName} (Id, ApplicationName, Entity, PrimaryKey, Category, Description, Filename, FileType, CreatedDateUtc,CreatedBy,CreatedByName, FileContent) VALUES (@id, @applicationName, @entity, @primaryKey, @category, @description, @filename, @fileType, @createdDateUtc,@createdBy,@createdByName, @fileContent)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", idPrimarykey);
                cmd.Parameters.AddWithValue("applicationName", _applicationName);
                cmd.Parameters.AddWithValue("entity", entity);
                cmd.Parameters.AddWithValue("primaryKey", primaryKey);
                cmd.Parameters.AddWithValue("category", category != null ? category : DBNull.Value);
                cmd.Parameters.AddWithValue("description", description != null ? description : DBNull.Value);
                cmd.Parameters.AddWithValue("filename", filename);
                cmd.Parameters.AddWithValue("fileType", fileType);
                cmd.Parameters.AddWithValue("createdDateUtc", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("createdBy", createdBy != null ? createdBy : DBNull.Value);
                cmd.Parameters.AddWithValue("createdByName", createdByName != null ? createdByName : DBNull.Value);
                cmd.Parameters.AddWithValue("fileContent", fileContent);

                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }

            return idPrimarykey;
        }

    }
}
