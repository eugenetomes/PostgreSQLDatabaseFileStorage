using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.Internal.Interface
{
    internal interface ICreateDatabaseTablesService
    {
        Task CreateFileStorageTableIfNotExists(string schemaName, string tableName);
        Task CreateSchema(string schemaName);
        Task<bool> SchemaExists(string schemaName);
        Task<bool> TableExists(string schemaName, string tableName);
        Task CreateScemaAndTableIfNotExists(string schemaName, string tableName);
    }
}
