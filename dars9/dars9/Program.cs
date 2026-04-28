namespace dars9;

internal class Program
{
    static async Task Main(string[] args)
    {
        string sourcePath = "";
        string destinationPath = "";

        await CopyLargeFileAsync(sourcePath, destinationPath);
    }
    public static async Task CopyLargeFileAsync(string sourcePath, string destinationPath)
    {
        const int bufferSize = 1024 * 1024 * 10; // 10 MB

        using (FileStream sourceStream = new FileStream(
            sourcePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize,
            useAsync: true))

        using (FileStream destinationStream = new FileStream(
            destinationPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize,
            useAsync: true))
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await destinationStream.WriteAsync(buffer, 0, bytesRead);
            }
        }
    }
}
