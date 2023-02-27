using FluentAssertions;
using NUnit.Framework;
using PostgreSQL.FileStorage.Core.Internal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PostgreSQL.FileStorage.Core.IntegrationTests.InitialSetup;

namespace PostgreSQL.FileStorage.Core.IntegrationTests.Internal.Service
{
    internal class CreateDatabaseTablesServiceTests
    {



        //[Test]
        //[RunIfDatabaseIsSetup]
        //public async Task CheckIfSchemeExists_ShouldPass_WhenValidSchemaPassed()
        //{
        //    var databaseService = new CreateDatabaseTablesService(TestHelper.GetDefaultConnectionString());

        //    var resultExists = await databaseService.SchemaExists("public");
        //    var resultNotExists = await databaseService.SchemaExists($"public_{Guid.NewGuid().ToString()}");

        //    resultExists.Should().BeTrue();
        //    resultNotExists.Should().BeFalse();
        //}

        //[Test]
        //[RunIfDatabaseIsSetup]
        //public async Task CreateScheme_ShouldPass_WhenValidNamePassed()
        //{
        //    var databaseService = new CreateDatabaseTablesService(TestHelper.GetDefaultConnectionString());
        //    var schemaName = $"test_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
        //    await databaseService.CreateSchema(schemaName);

        //    var resultExists = await databaseService.SchemaExists(schemaName);
        //    resultExists.Should().BeTrue();
        //}

        //[Test]
        //[RunIfDatabaseIsSetup]
        //public async Task InternalText()
        //{
        //    var databaseService = new CreateDatabaseTablesService(TestHelper.GetDefaultConnectionString());
        //    await databaseService.CreateFileStorageTableIfNotExists("public", "ETCodeTest");

        //}

    }
}
