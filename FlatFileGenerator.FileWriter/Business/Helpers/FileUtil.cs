namespace FlatFileGenerator.FileWriter.Business.Helpers
{
    public class FileUtil
    {
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
