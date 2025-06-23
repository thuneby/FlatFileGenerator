namespace FlatFileGenerator.FileWriter.Business.Helpers
{
    public class FileUtil
    {
        public async Task<byte[]> UploadFile(string fileName, string path) 
        {
            var fullPath = Path.Combine(path, fileName);
            var fileInfo = new FileInfo(fullPath);
            var content = new byte[fileInfo.Length];
            await using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            await stream.ReadExactlyAsync(content, 0, (int)fileInfo.Length);
            return content;
        }

        public async Task<string> ReadFileAsync(string filePath)
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

        public async Task<bool> WriteFileAsync(string content, string fileName, string filePath = "C:\\Temp" )
        {
            
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(fileName));
            }
            var fullPath = Path.Combine(filePath, fileName);
            try
            {
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(filePath);
                }
                await using var writer = new StreamWriter(fullPath, false);
                await writer.WriteAsync(content);
            }
            catch (Exception ex)
            {
                // Handle exceptions such as access denied, disk full, etc.
                throw new IOException($"Error writing to file at {fullPath}", ex);
            }
            return true;
        }

        public async Task<bool> WriteFileAsync(byte[] payload, string fileName, string filePath = "C:\\Temp")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(fileName));
            }
            var fullPath = Path.Combine(filePath, fileName);
            await File.WriteAllBytesAsync(fullPath, payload);
            return true;
        }

    }
}
