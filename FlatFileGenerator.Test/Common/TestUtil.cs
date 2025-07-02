using FlatFileGenerator.Utilities.Files;

namespace FlatFileGenerator.Test.Common
{
    public static class TestUtil
    {
        private const string DataPath = @"..\..\..\TestData\";

        public static async Task<byte[]> ReadFileAsync(string fileName)
        {
            return await FileUtil.ReadFileAsync(fileName, DataPath);
        }
    }
}