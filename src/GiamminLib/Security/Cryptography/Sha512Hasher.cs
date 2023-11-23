using System;
using System.Security.Cryptography;
using System.Text;
using GiamminLib.DomainModels;

namespace GiamminLib.Security.Cryptography;

public class Sha512Hasher:IHasher
{
    private readonly Encoding _encoding;
    private readonly int _saltSize;

    public Sha512Hasher():this(Encoding.Unicode){ }
    public Sha512Hasher(Encoding encoding, int saltSize=Constants.DefaultSaltSize)
    {
        _encoding = encoding;
        _saltSize = saltSize;
    }
    public string Encrypt(string stringToEncrypt)
    {
        if (string.IsNullOrEmpty(stringToEncrypt))
        {
            throw new ArgumentNullException(nameof(stringToEncrypt));
        }
        var salt = HashUtils.GenerateSalt(_saltSize);
        return Encrypt(stringToEncrypt, salt);
    }
    public bool Verify(string notEncrypted, string encrypted)
    {
        if (string.IsNullOrEmpty(notEncrypted))
        {
            throw new ArgumentNullException(nameof(notEncrypted));
        }
        if (string.IsNullOrEmpty(encrypted))
        {
            throw new ArgumentNullException(nameof(encrypted));
        }

        //tiro fuori il salt
        var hashData = Convert.FromBase64String(encrypted);
        var salt = new byte[_saltSize];
        Array.Copy(hashData, hashData.Length - salt.Length, salt, 0, salt.Length);

        var hashedClearString = Encrypt(notEncrypted, salt);

        return StringComparer.Ordinal.Compare(encrypted, hashedClearString) == 0;
    }
    /// <summary>
    /// Encrypts the <param ref="original"></param> using the specified algorithm (T) and adding specified salt <param ref="salt"></param>
    /// </summary>
    /// <param name="original">The string to encript.</param>
    /// <param name="salt">the salt</param>
    /// <returns>the hashed string with trailing salt value in Base64 format</returns>
    public string Encrypt(string original, byte[] salt)
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
        var originalData = _encoding.GetBytes(original);
        var hashSaltData = Encrypt(originalData, salt);
        return Convert.ToBase64String(hashSaltData);
    }
        
    /// <summary>
    /// Encrypts the <param ref="original"></param> using the specified algorithm (T) and adding specified salt <param ref="salt"></param>
    /// </summary>
    /// <param name="original">The string to encript.</param>
    /// <param name="salt">the salt</param>
    /// <returns>the hashed bytes with trailing salt value</returns>
    public byte[] Encrypt(byte[] original, byte[] salt)
    {
        // Append the salt to the end of the original
        var saltedPasswordData = new byte[original.Length + salt.Length];
        Array.Copy(original, 0, saltedPasswordData, 0, original.Length);
        Array.Copy(salt, 0, saltedPasswordData, original.Length, salt.Length);

        byte[] hashData;
        using (var sha = SHA512.Create())
        {
            hashData = sha.ComputeHash(saltedPasswordData);
        }

        var hashSaltData = new byte[hashData.Length + salt.Length];
        Array.Copy(hashData, 0, hashSaltData, 0, hashData.Length);
        Array.Copy(salt, 0, hashSaltData, hashData.Length, salt.Length);

        return hashSaltData;
    }
}