namespace GiamminLib.DomainModels;

public interface IHasher
{
    /// <summary>
    /// Encrypt some text and return a string
    /// </summary>
    /// <param name="stringToEncrypt">The string to encrypt.</param>
    /// <returns></returns>
    string Encrypt(string stringToEncrypt);

    /// <summary>
    /// verify equals 
    ///  </summary>
    /// <param name="notEncrypted"></param>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    bool Verify(string notEncrypted, string encrypted);
}