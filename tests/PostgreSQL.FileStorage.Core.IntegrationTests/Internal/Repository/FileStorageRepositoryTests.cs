using NUnit.Framework;
using PostgreSQL.FileStorage.Core.Internal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PostgreSQL.FileStorage.Core.IntegrationTests.InitialSetup;

namespace PostgreSQL.FileStorage.Core.IntegrationTests.Internal.Repository
{
    internal class FileStorageRepositoryTests
    {

        //[Test]
        //[RunIfDatabaseIsSetup]
        //public async Task GetFileContentsById_ShouldPass_WhenValidIdPassed()
        //{
        //    var repository = new FileStorageRepository(TestHelper.GetDefaultConnectionString(), "public", "testfile", "TESTAPP");

        //    var result = await repository.GetFileContentsById(Guid.Parse("57bfad91-5635-432f-acbe-022605f9609a"), CancellationToken.None);

        //    var resultList = await repository.GetFileInformationByEntityAndPrimaryKey("Instruction", "1");
        //    var et = resultList.ToList();
        //}


        //[Test]
        //[RunIfDatabaseIsSetup]
        //public async Task GetFileInformationById_ShouldPass_WhenValidIdPassed()
        //{
        //    var repository = new FileStorageRepository(TestHelper.GetDefaultConnectionString(), "public", "testfile", "TESTAPP");

        //    var result = await repository.GetFileInformationById(Guid.Parse("57bfad91-5635-432f-acbe-022605f9609a"));
        //}

    }
}
