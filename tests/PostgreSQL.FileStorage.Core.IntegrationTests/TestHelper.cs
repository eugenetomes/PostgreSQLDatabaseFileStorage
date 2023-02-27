using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using PostgreSQL.FileStorage.Core.Model;
using System.Text;

namespace PostgreSQL.FileStorage.Core.IntegrationTests
{
    internal static class TestHelper
    {
        public static bool RunIfDatabaseIsSetup()
        {
            var config = GetConfigurationFileRoot();
            var runIfDatabaseIsSetup = config.RunIfDatabaseIsSetup;
            return runIfDatabaseIsSetup;
        }

        public static Config GetConfigurationFileRoot()
        {
            var config = new ConfigurationBuilder()
                   .AddJsonFile($"appsettings.json", true, true)
                   .AddJsonFile($"appsettings.local.json", true, true)
                   .Build()
                   .Get<Config>();

            return config;
        }

        public static IOptions<Config> GetConfigurationOptions()
        {
            var config = GetConfigurationFileRoot();
            return Options.Create(config);
        }

        public static FileStorageConfiguration GetCurrentFileStorageConfiguration()
        {
            var config = TestHelper.GetConfigurationFileRoot().FileStorageConfiguration;
            if (config == null)
            {
                throw new NullReferenceException();
            }
            return config;
        }

        public static string GetGrootFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IAmGroot.jpg");
        }

        public static byte[] GetGrootByteArray()
        {
            var fileContents = File.ReadAllBytes(GetGrootFilePath());
            return fileContents;
        }

        public static string RandomText(int minLength, int maxLength)
        {
            var Rand = new Random();
            var PunctuationCharacters = @" .,\/;:-".ToCharArray();
            var AlphaNumericCharacters = @"0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var sb = new StringBuilder();

            var stringLength = Rand.Next(minLength, maxLength);

            for (int i = 0; i < stringLength; i++)
            {
                if (Rand.NextDouble() < 0.10)
                {
                    // add punctuation
                    sb.Append(PunctuationCharacters[Rand.Next(0, PunctuationCharacters.Length - 1)]);
                }
                else
                {
                    // add characters
                    sb.Append(AlphaNumericCharacters[Rand.Next(0, AlphaNumericCharacters.Length - 1)]);
                }
            }

            return sb.ToString();
        }
    }
}