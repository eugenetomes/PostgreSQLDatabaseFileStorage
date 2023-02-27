using PostgreSQL.FileStorage.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.IntegrationTests
{
    internal class Config
    {
        public bool RunIfDatabaseIsSetup { get; set; }
        public FileStorageConfiguration? FileStorageConfiguration { get; set; }
    }
}
