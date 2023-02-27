using Microsoft.Extensions.Options;
using NUnit.Framework;
using PostgreSQL.FileStorage.Core.Interface;
using PostgreSQL.FileStorage.Core.Model;
using PostgreSQL.FileStorage.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.IntegrationTests.Service
{
    internal class FileStorageServiceTests
    {
        [Test]
        public async Task InternalTest()
        {
            var config = GetFileStorageConfiguration();
            IFileStorageService service = new FileStorageService(config);

            var result = await service.AddFile("Instruction", "1", @"D:\Code\Docker\compose.yml", null, "category", "ET", "EugeneTomes", CancellationToken.None);

        }

        private IOptions<FileStorageConfiguration> GetFileStorageConfiguration()
        {
            var fileStorageConfiguration = new FileStorageConfiguration
            {
                ApplicationName = "TESTAPP",
                ConnectionString = TestHelper.GetDefaultConnectionString(),
                FileTableName = "TestFile",
                SchemaName = "public"
            };
            return Options.Create(fileStorageConfiguration);
        }
    }
}
