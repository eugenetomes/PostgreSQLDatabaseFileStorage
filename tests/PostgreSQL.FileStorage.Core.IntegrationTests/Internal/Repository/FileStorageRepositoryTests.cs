using FluentAssertions;
using NUnit.Framework;
using PostgreSQL.FileStorage.Core.Internal.Repository;
using PostgreSQL.FileStorage.Core.Internal.Service;
using PostgreSQL.FileStorage.Core.Model;
using static Dapper.SqlMapper;

namespace PostgreSQL.FileStorage.Core.IntegrationTests.Internal.Repository
{
    internal class FileStorageRepositoryTests
    {

        private record CreatedRecord(Guid id, string entity, string primaryKey, string filename, string fileType, byte[] fileContent, string? description, string? category, string? createdBy, string? createdByName, DateTime dateCreatedUtc);

        public FileStorageRepositoryTests()
        {
            if (TestHelper.RunIfDatabaseIsSetup())
            {
                var config = TestHelper.GetCurrentFileStorageConfiguration();
                var service = new CreateDatabaseTablesService(config.ConnectionString);
                service.CreateScemaAndTableIfNotExists(config.SchemaName, config.FileTableName).GetAwaiter().GetResult(); ;
            }
        }

        //[Test]
        //[RunIfDatabaseIsSetup]
        //public async Task GetFileContentsById_ShouldPass_WhenValidIdPassed()
        //{
        //    var repository = new FileStorageRepository(TestHelper.GetDefaultConnectionString(), "public", "testfile", "TESTAPP");

        //    var result = await repository.GetFileContentsById(Guid.Parse("57bfad91-5635-432f-acbe-022605f9609a"), CancellationToken.None);

        //    var resultList = await repository.GetFileInformationByEntityAndPrimaryKey("Instruction", "1");
        //    var et = resultList.ToList();
        //}


        [Test]
        [RunIfDatabaseIsSetup]
        public async Task GetFileInformationById_ShouldReturnDefault_WhenInValidIdPassed()
        {
            var repository = GetFileRepository();
            var id = Guid.NewGuid();

            var result = await repository.GetFileInformationById(id);

            result.Should().Be(default);

        }

        [Test]
        [RunIfDatabaseIsSetup]
        public async Task GetFileInformationById_ShouldPass_WhenValidIdPassed()
        {
            var repository = GetFileRepository();
            var record = await CreateRandomFileRecord();

            var result = await repository.GetFileInformationById(record.id);

            result.Should().BeOfType<FileContentBasicModel>();
            result.Id.Should().Be(record.id);
            result.Entity.Should().Be(record.entity);
            result.PrimaryKey.Should().Be(record.primaryKey);
            result.FileType.Should().Be(record.fileType);
            result.Filename.Should().Be(record.filename);
            result.Description.Should().Be(record.description);
            result.PrimaryKey.Should().Be(record.primaryKey);
            result.Category.Should().Be(record.category);
            result.CreatedBy.Should().Be(record.createdBy);
            result.CreatedByName.Should().Be(record.createdByName);
            result.CreatedDateUtc.Should().BeCloseTo(record.dateCreatedUtc, new TimeSpan(0, 0, 5));
        }


        [Test]
        [RunIfDatabaseIsSetup]
        public async Task GetFileInformationByEntityAndPrimaryKey_ShouldPass_WhenValidEntityKeyPassed()
        {
            var repository = GetFileRepository();
            var record = await CreateRandomFileRecord();

            var result = await repository.GetFileInformationByEntityAndPrimaryKey(record.entity, record.primaryKey);
            var firstResult = result.FirstOrDefault();

            firstResult.Should().BeOfType<FileContentBasicModel>();
            firstResult.Id.Should().Be(record.id);
            firstResult.Entity.Should().Be(record.entity);
            firstResult.PrimaryKey.Should().Be(record.primaryKey);
            firstResult.FileType.Should().Be(record.fileType);
            firstResult.Filename.Should().Be(record.filename);
            firstResult.Description.Should().Be(record.description);
            firstResult.PrimaryKey.Should().Be(record.primaryKey);
            firstResult.Category.Should().Be(record.category);
            firstResult.CreatedBy.Should().Be(record.createdBy);
            firstResult.CreatedByName.Should().Be(record.createdByName);
            firstResult.CreatedDateUtc.Should().BeCloseTo(record.dateCreatedUtc, new TimeSpan(0, 0, 5));

        }

        [Test]
        [RunIfDatabaseIsSetup]
        public async Task GetFileInformationByEntityAndPrimaryKey_ShouldReturnEmpty_WhenIvlaidValidEntityKeyPassed()
        {
            var repository = GetFileRepository();
            var entity = TestHelper.RandomText(10, 15);
            var primaryKey = TestHelper.RandomText(10, 15);

            var result = await repository.GetFileInformationByEntityAndPrimaryKey(entity, primaryKey);

            result.Should().BeEmpty();
        }

        [Test]
        [RunIfDatabaseIsSetup]
        public async Task GetFileContentsById_ShouldReturnValidEntity_WhenValidIdPassed()
        {
            var repository = GetFileRepository();
            var record = await CreateRandomFileRecord();

            var result = await repository.GetFileContentsById(record.id, CancellationToken.None);

            result.FileName.Should().Be(record.filename);
            ByteArraysEqual(result.FileContent, record.fileContent).Should().BeTrue();           
        }

        [Test]
        [RunIfDatabaseIsSetup]
        public async Task GetFileContentsById_ShouldReturnDefault_WhenInValidIdPassed()
        {
            var repository = GetFileRepository();

            var result = await repository.GetFileContentsById(Guid.NewGuid(), CancellationToken.None);

            result.Should().Be(default);
        }

        [Test]
        [RunIfDatabaseIsSetup]
        public async Task Delete_ShouldDeleteRecord_IfIdExists()
        {
            var repository = GetFileRepository();
            var record = await CreateRandomFileRecord();

            await repository.Delete(record.id, CancellationToken.None);
            var result = await repository.GetFileInformationById(record.id);

            result.Should().Be(default);
        }

        [Test]
        [RunIfDatabaseIsSetup]
        public async Task Create_ShouldReturnId_IfValid()
        {
            var repository = GetFileRepository();
            var record = await CreateRandomFileRecord();

            var result = await repository.GetFileInformationById(record.id);

            result.Should().BeOfType<FileContentBasicModel>();
            result.Id.Should().Be(record.id);
            result.Entity.Should().Be(record.entity);
            result.PrimaryKey.Should().Be(record.primaryKey);
            result.FileType.Should().Be(record.fileType);
            result.Filename.Should().Be(record.filename);
            result.Description.Should().Be(record.description);
            result.PrimaryKey.Should().Be(record.primaryKey);
            result.Category.Should().Be(record.category);
            result.CreatedBy.Should().Be(record.createdBy);
            result.CreatedByName.Should().Be(record.createdByName);
            result.CreatedDateUtc.Should().BeCloseTo(record.dateCreatedUtc, new TimeSpan(0, 0, 5));
        }



        private async Task<CreatedRecord> CreateRandomFileRecord()
        {
            var repository = GetFileRepository();
            var entity = TestHelper.RandomText(20, 20);
            var primaryKey = TestHelper.RandomText(20, 20);
            var fileType = ".jpg";
            var filename = $"{TestHelper.RandomText(20, 20)}{fileType}";
            var fileContent = TestHelper.GetGrootByteArray();
            var description = TestHelper.RandomText(20, 20);
            var category = TestHelper.RandomText(20, 20);
            var createdBy = TestHelper.RandomText(20, 20);
            var createdByName = TestHelper.RandomText(20, 20);


            var id = await repository.Create(entity, primaryKey, filename, fileType, fileContent, description, category, createdBy, createdByName, CancellationToken.None);
            var dateCreatedUtc = DateTime.UtcNow;

            return new CreatedRecord(id, entity, primaryKey, filename, fileType, fileContent, description, category, createdBy, createdByName, dateCreatedUtc);
        }

        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        private FileStorageRepository GetFileRepository()
        {
            var config = TestHelper.GetCurrentFileStorageConfiguration();
            return new FileStorageRepository(config.ConnectionString, config.SchemaName, config.FileTableName, config.ApplicationName);
        }

    }
}
