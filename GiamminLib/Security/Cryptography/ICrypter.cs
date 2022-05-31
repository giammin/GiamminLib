namespace GiamminLib.Security.Cryptography
{
    public interface ICrypter:IHasher
    {
        /// <summary>
        /// Encrypt some text and return a string
        /// </summary>
        /// <param name="encryptedString">The string to decrypt.</param>
        /// <returns></returns>
        string Decrypt(string encryptedString);
    }
}