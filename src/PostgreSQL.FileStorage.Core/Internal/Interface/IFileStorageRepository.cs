using PostgreSQL.FileStorage.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.Internal.Interface
{
    internal interface IFileStorageRepository
    {
        Task<FileContentBasicModel> GetFileInformationById(Guid id);
        Task<IEnumerable<FileContentBasicModel>> GetFileInformationByEntityAndPrimaryKey(string entity, string primaryKey);
        Task<FileContentModel> GetFileContentsById(Guid id, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
        Task<Guid> Create(string entity, string primaryKey, string filename, string fileType, byte[] fileContent, string? description, string? category, string? createdBy, string? createdByName, CancellationToken cancellationToken);
    }
}
