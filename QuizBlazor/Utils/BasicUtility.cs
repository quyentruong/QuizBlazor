using System.IO.Compression;

namespace QuizBlazor.Utils;

public class BasicUtility
{
    public static string DecompressStringDeflate(string compressedText)
    {
        byte[] compressedData = Convert.FromBase64String(compressedText);

        using var memoryStream = new MemoryStream(compressedData);
        using var gzipStream = new DeflateStream(memoryStream, CompressionMode.Decompress);

        using var streamReader = new StreamReader(gzipStream);
        return streamReader.ReadToEnd();
    }
}