using System;
using System.Security.Cryptography;
using System.Text;

namespace GiamminLib.Security.Cryptography;

/// <summary>
/// Utility class for generating hashes
/// </summary>
public static class HashUtils
{
    /// <summary>
    /// Encrypts the <param ref="original"></param> using the specified algorithm (T) and adding specified salt <param ref="salt"></param>
    /// </summary>
    /// <typeparam name="T">the hashing algorithm to use</typeparam>
    /// <param name="original">The string to encript.</param>
    /// <param name="salt">the salt</param>
    /// <param name="encoding">the encoding to use for read the original string</param>
    /// <returns>the hashed string with trailing salt value in Base64 format</returns>
    public static string Encrypt<T>(string original, byte[] salt, Encoding encoding) where T:HashAlgorithm,new()
    {
        if (string.IsNullOrEmpty(original))
        {
            throw new ArgumentNullException(nameof(original));
        }
        if (salt == null)
        {
            throw new ArgumentNullException(nameof(salt));
        }
        // Convert the string original value to a byte array
        var originalData = encoding.GetBytes(original);
        var hashSaltData = Encrypt<T>(originalData, salt);
        return Convert.ToBase64String(hashSaltData);
    }

    /// <summary>
    /// Generate the hash for the specified <param ref="original"></param> using the specified algorithm (T)
    /// </summary>
    /// <typeparam name="T">the hashing algorithm to use</typeparam>
    /// <param name="original">the array of bytes to Hash.</param>
    /// <returns>the checksum string </returns>
    public static string GetChecksum<T>(byte[] original) where T : HashAlgorithm, new()
    {
        var hash = Encrypt<T>(original, new byte[0]);

        var sb = new StringBuilder(hash.Length * 2);

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Encrypts the <param ref="original"></param> using the specified algorithm (T) and adding specified salt <param ref="salt"></param>
    /// </summary>
    /// <typeparam name="T">the hashing algorithm to use</typeparam>
    /// <param name="original">The string to encript.</param>
    /// <param name="salt">the salt</param>
    /// <returns>the hashed bytes with trailing salt value</returns>
    public static byte[] Encrypt<T>(byte[] original, byte[] salt) where T : HashAlgorithm, new()
    {
        // Append the salt to the end of the original
        var saltedPasswordData = new byte[original.Length + salt.Length];
        Array.Copy(original, 0, saltedPasswordData, 0, original.Length);
        Array.Copy(salt, 0, saltedPasswordData, original.Length, salt.Length);

        byte[] hashData;
        using (var sha = new T())
        {
            hashData = sha.ComputeHash(saltedPasswordData);
        }

        var hashSaltData = new byte[hashData.Length + salt.Length];
        Array.Copy(hashData, 0, hashSaltData, 0, hashData.Length);
        Array.Copy(salt, 0, hashSaltData, hashData.Length, salt.Length);

        return hashSaltData;
    }

    /// <summary>
    /// Encrypts the <param ref="original"></param> using the specified algorithm (T) and adding salt
    /// </summary>
    /// <typeparam name="T">the hashing algorithm to use</typeparam>
    /// <param name="original">The string to encript.</param>
    /// <param name="saltSize">Size of the salt. if 0 no salt is added</param>
    /// <param name="encoding">the encoding to use for read the original string</param>
    /// <returns>the hashed string with trailing salt value in Base64 format</returns>
    public static string Encrypt<T>(string original, Encoding encoding, int saltSize = 8) where T : HashAlgorithm, new()
    {
        var salt = GenerateSalt(saltSize);
        return Encrypt<T>(original, salt,encoding);
    }

    /// <summary>
    /// Generate a salt using a cryptographically secure random number generator
    /// </summary>
    /// <param name="saltSize">Size of the salt.</param>
    public static byte[] GenerateSalt(int saltSize)
    {
        var rtn = new byte[saltSize];
        var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(rtn);
        return rtn;
    }

    /// <summary>
    /// Verifies that <param ref="hash"></param> belong to <param ref="clearString"> </param>using the specified algorithm (T)
    /// </summary>
    /// <typeparam name="T">the hashing algorithm to use</typeparam>
    /// <param name="clearString">The not hashed string.</param>
    /// <param name="hash">The hash with trailing salt of size <param ref="saltSize"></param></param>
    /// <param name="encoding">the encoding to use for read the original string</param>
    /// <param name="saltSize">Size of the salt. 0 if no salt is used</param>
    /// <returns>true if <param ref="hash"></param> belong to <param ref="clearString"></param></returns>
    public static bool Verify<T>(string clearString, string hash, Encoding encoding , int saltSize = 8) where T : HashAlgorithm, new()
    {
        //tiro fuori il salt
        var hashData = Convert.FromBase64String(hash);
        var salt = new byte[saltSize];
        Array.Copy(hashData, hashData.Length - salt.Length, salt, 0, salt.Length);

        var hashedclearString = Encrypt<T>(clearString, salt,encoding);

        return StringComparer.Ordinal.Compare(hash, hashedclearString) == 0;
    }
}