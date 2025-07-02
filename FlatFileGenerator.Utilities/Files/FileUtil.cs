namespace FlatFileGenerator.Utilities.Files
{
    public class FileUtil
    {
        public static async Task<byte[]> ReadFileAsync(string fileName, string dataPath)
        {
            var fullPath = System.IO.Path.Combine(dataPath, fileName);
            var fileInfo = new FileInfo(fullPath);
            var content = new byte[fileInfo.Length];
            await using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            await stream.ReadExactlyAsync(content, 0, (int)fileInfo.Length);
            return content;
        }

        public async Task<byte[]> UploadFile(string fileName, string path)
        {
            var fullPath = Path.Combine(path, fileName);
            var fileInfo = new FileInfo(fullPath);
            var content = new byte[fileInfo.Length];
            await using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            await stream.ReadExactlyAsync(content, 0, (int)fileInfo.Length);
            return content;
        }

        public async Task<string> ReadFileAsStringAsync(string filePath) 
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions such as file not found, access denied, etc.
                throw new IOException($"Error reading file at {filePath}", ex);
            }
        }
    }
}
