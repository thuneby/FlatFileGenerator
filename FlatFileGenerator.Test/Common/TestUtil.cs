namespace FlatFileGenerator.Test.Common
{
    public class TestUtil
    {
        private const string DataPath = @"..\..\..\TestData\";

        public static async Task<byte[]> ReadFileAsync(string fileName)
        {
            var fullPath = System.IO.Path.Combine(DataPath, fileName);
            var fileInfo = new FileInfo(fullPath);
            var content = new byte[fileInfo.Length];
            await using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            await stream.ReadExactlyAsync(content, 0, (int)fileInfo.Length);
            return content;
        }
    }
}