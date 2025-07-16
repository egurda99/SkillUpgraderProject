using System.IO;
using System.Security.Cryptography;
using System.Text;

public sealed class AesEncryptor
{
    private readonly byte[] Key = Encoding.ASCII.GetBytes("MySecretKey12345"); // 16 symbols
    private readonly byte[] IV = Encoding.ASCII.GetBytes("InitVector123456"); // 16 symbols

    public byte[] Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using var sw = new StreamWriter(cs);
        sw.Write(plainText);
        sw.Close();
        return ms.ToArray();
    }

    public string Decrypt(byte[] cipherBytes)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipherBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}
