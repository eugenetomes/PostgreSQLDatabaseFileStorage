using PostgreSQL.FileStorage.Core.Model;

namespace PostgreSQL.FileStorage.Core.Interface
{
    public interface IFileStorageService
    {

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
        Task<Guid> AddFile(string entity, string primaryKey, string fileName, string fileType, byte[] fileContents, string? description, string? category, string? createdBy, string? createdByName, CancellationToken cancellationToken);

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
        Task<Guid> AddFile(string entity, string primaryKey, string fullFilePath, string? description, string? category, string? createdBy, string? createdByName, CancellationToken cancellationToken);


        /// <summary>
        /// Delete a specific file record from the Database
        /// </summary>
        /// <param name="id">Unique Id for the file record</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task DeleteFile(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve the file contents as well as the filename for a specific File records
        /// </summary>
        /// <param name="id">Unique Id for the file record</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Returns the file contents in byte array as well as the file name</returns>
        Task<FileContentModel> GetFileContentsById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve basic File information for the entity and primary key passed 
        /// </summary>
        /// <param name="entity">The Entity that is used to store the file information</param>
        /// <param name="primaryKey">The Unique Primary Key that is stored against the entity</param>
        /// <returns></returns>
        Task<IEnumerable<FileContentBasicModel>> GetFileInformationByEntityAndPrimaryKey(string entity, string primaryKey);

        /// <summary>
        /// Retrieve basic File information for the Id passed 
        /// </summary>
        /// <param name="id">Unique Id for the file record</param>
        /// <returns></returns>
        Task<FileContentBasicModel> GetFileInformationById(Guid id);
    }
}
