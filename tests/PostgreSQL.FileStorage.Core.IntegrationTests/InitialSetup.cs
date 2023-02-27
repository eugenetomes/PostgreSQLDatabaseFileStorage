using NUnit.Framework.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.IntegrationTests
{
    internal class InitialSetup
    {
        



        public class RunIfDatabaseIsSetupAttribute : Attribute, ITestAction
        {
            public ActionTargets Targets { get; private set; }

            public void AfterTest(ITest test) { }

            public void BeforeTest(ITest test)
            {
                if (!TestHelper.RunIfDatabaseIsSetup())
                {
                    Assert.Ignore("Omitting {0}. Database is not setup.", test.Name);
                }
            }
        }
    }
}
