using System.Security.Cryptography;

namespace Work_with_orders.Common;

public static class Cryptography
{
    private const int HashSize = 20;
    private const int SaltSize = 16;
    private const int IterationsCount = 10203;

    private static byte[] _salt;
    private static byte[] _hash;
    private static byte[] _hashBytes;

    public static string Hash(string password)
    {
        GenerateSalt();
        GenerateHash(password);
        CombineBytes();
        return GetStringFromCombineBytes();
    }

    private static byte[] GenerateSalt()
    {
        new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
        return _salt;
    }

    public static bool Verify(this string text, string hashPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashPassword);

        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        var pbkdf2 = new Rfc2898DeriveBytes(text, salt, IterationsCount);
        byte[] hash = pbkdf2.GetBytes(HashSize);

        for (int i = 0; i < 20; i++)
            if (hashBytes[i + 16] != hash[i])
                return false;

        return true;
    }

    private static byte[] GenerateHash(string password)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, IterationsCount);
        return _hash = pbkdf2.GetBytes(HashSize);
    }

    private static byte[] CombineBytes()
    {
        _hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(_salt, 0, _hashBytes, 0, SaltSize);
        Array.Copy(_hash, 0, _hashBytes, SaltSize, HashSize);
        return _hashBytes;
    }

    private static string GetStringFromCombineBytes() => Convert.ToBase64String(_hashBytes);
}