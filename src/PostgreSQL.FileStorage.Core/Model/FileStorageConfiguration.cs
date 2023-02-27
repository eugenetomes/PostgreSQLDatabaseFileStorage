using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.Model
{
    public class FileStorageConfiguration
    {
        public string ConnectionString { get; set; }
        public string ApplicationName { get; set; }
        public string SchemaName { get; set; }
        public string FileTableName { get; set; }
    }
}
