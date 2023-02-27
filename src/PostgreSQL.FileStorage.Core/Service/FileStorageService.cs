using Microsoft.Extensions.Options;
using PostgreSQL.FileStorage.Core.Interface;
using PostgreSQL.FileStorage.Core.Internal.Interface;
using PostgreSQL.FileStorage.Core.Internal.Repository;
using PostgreSQL.FileStorage.Core.Internal.Service;
using PostgreSQL.FileStorage.Core.Model;

namespace PostgreSQL.FileStorage.Core.Service
{
    public class FileStorageService : IFileStorageService
    {
        private readonly FileStorageConfiguration _fileStorageConfiguration;
        private readonly ICreateDatabaseTablesService _createDatabaseTablesService;
        private readonly IFileStorageRepository _fileStorageRepository;

        public FileStorageService(IOptions<FileStorageConfiguration> fileStorageConfiguration)
        {
            _fileStorageConfiguration = fileStorageConfiguration.Value;
            _createDatabaseTablesService = new CreateDatabaseTablesService(_fileStorageConfiguration.ConnectionString);
            _createDatabaseTablesService.CreateScemaAndTableIfNotExists(_fileStorageConfiguration.SchemaName, _fileStorageConfiguration.FileTableName).GetAwaiter().GetResult();
            _fileStorageRepository = new FileStorageRepository(_fileStorageConfiguration.ConnectionString, _fileStorageConfiguration.SchemaName, _fileStorageConfiguration.FileTableName, _fileStorageConfiguration.ApplicationName);
        }


        /// <summary>
        /// Retrieve basic File information for the Id passed 
        /// </summary>
        /// <param name="id">Unique Id for the file record</param>
        /// <returns></returns>
        public async Task<FileContentBasicModel> GetFileInformationById(Guid id)
        {
            var result = await _fileStorageRepository.GetFileInformationById(id);
            return result;
        }


        /// <summary>
        /// Retrieve basic File information for the entity and primary key passed 
        /// </summary>
        /// <param name="entity">The Entity that is used to store the file information</param>
        /// <param name="primaryKey">The Unique Primary Key that is stored against the entity</param>
        /// <returns></returns>
        public async Task<IEnumerable<FileContentBasicModel>> GetFileInformationByEntityAndPrimaryKey(string entity, string primaryKey)
        {
            var result = await _fileStorageRepository.GetFileInformationByEntityAndPrimaryKey(entity, primaryKey);
            return result;
        }


        /// <summary>
        /// Retrieve the file contents as well as the filename for a specific File records
        /// </summary>
        /// <param name="id">Unique Id for the file record</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Returns the file contents in byte array as well as the file name</returns>
        public async Task<FileContentModel> GetFileContentsById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _fileStorageRepository.GetFileContentsById(id, cancellationToken);
            return result;
        }


        /// <summary>
        /// Delete a specific file record from the Database
        /// </summary>
        /// <param name="id">Unique Id for the file record</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task DeleteFile(Guid id, CancellationToken cancellationToken)
        {
            await _fileStorageRepository.Delete(id, cancellationToken);
        }


        /// <summary>
        /// Add a file to the database
        /// </summary>
        /// <param name="entity">The Entity that is used to store the file information</param>
        /// <param name="primaryKey">The Unique Primary Key that is stored against the entity</param>
        /// <param name="fullFilePath">Full file path of the file</param>
        /// <param name="description">Description for the specific file</param>
        /// <param name="category">Category for the file</param>
        /// <param name="createdBy">Id of who Created the Record</param>
        /// <param name="createdByName">name of who Created the Record</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Unique Identity of the record</returns>
        public async Task<Guid> AddFile(string entity, string primaryKey, string fullFilePath, string? description, string? category, string? createdBy, string? createdByName, CancellationToken cancellationToken)
        {
            var fileInfo = new FileInfo(fullFilePath);
            var fileContents = File.ReadAllBytes(fullFilePath);

            var idPrimarykey = await _fileStorageRepository.Create(entity, primaryKey, fileInfo.Name, fileInfo.Extension, fileContents, description, category, createdBy, createdByName, cancellationToken);

            return idPrimarykey;
        }


        /// <summary>
        /// Add a file to the database using a byte array as contents
        /// </summary>
        /// <param name="entity">The Entity that is used to store the file information</param>
        /// <param name="primaryKey">The Unique Primary Key that is stored against the entity</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="fileType">The File Type</param>
        /// <param name="fileContents">File Contents as byte array</param>
        /// <param name="description">Description for the specific file</param>
        /// <param name="category">Category for the file</param>
        /// <param name="createdBy">Id of who Created the Record</param>
        /// <param name="createdByName">name of who Created the Record</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<Guid> AddFile(string entity, string primaryKey, string fileName, string fileType, byte[] fileContents, string? description, string? category, string? createdBy, string? createdByName, CancellationToken cancellationToken)
        {

            var idPrimarykey = await _fileStorageRepository.Create(entity, primaryKey, fileName, fileType, fileContents, description, category, createdBy, createdByName, cancellationToken);

            return idPrimarykey;
        }
    }
}
