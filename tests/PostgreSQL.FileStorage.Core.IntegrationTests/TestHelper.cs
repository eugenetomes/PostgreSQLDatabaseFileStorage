using NUnit.Framework;

namespace PostgreSQL.FileStorage.Core.IntegrationTests
{
    public static class TestHelper
    {
        public static string GetDefaultConnectionString()
        {
            return "Server=127.0.0.1;Port=5432;Database=test;User Id=tomesdev;Password=tomesdev;";
        }

        public static bool RunIfDatabaseIsSetup()
        {
            return false;
        }
    }
}