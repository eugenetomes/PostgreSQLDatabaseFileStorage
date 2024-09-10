using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace PostgreSQL.FileStorage.Core.IntegrationTests;

public class RunIfDatabaseIsSetupAttribute : Attribute, ITestAction
{
    public ActionTargets Targets { get; private set; }

    public void AfterTest(ITest test) { }

    public void BeforeTest(ITest test)
    {
        if (!TestHelper.RunIfDatabaseIsSetup())
        {
            Assert.Ignore("Omitting {0}. Database is not setup.");
        }
    }
}
