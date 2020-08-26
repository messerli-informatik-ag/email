using System.IO;
using Messerli.FileSystem;

namespace Messerli.Email.Test
{
    internal static class IntegrationTestUtility
    {
        private const string ResourcesDirectoryName = "Resources";

        public static Stream OpenResourceFile(string filename)
            => new FileOpeningBuilder()
                .Read(true)
                .Open(Path.Combine(ResourcesDirectoryName, filename));
    }
}
