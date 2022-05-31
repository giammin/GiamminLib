using NUnit.Framework;
using GiamminLib.Security.Cryptography;

namespace GiamminLib.Tests;

[TestFixture]
public class Sha512HasherTests
{
    [Test]
    [TestCase("Lorem ipsum dolor sit amet,")]
    [TestCase("dfasdfa")]
    [TestCase("giammin")]
    [TestCase("12342354365567")]
    [TestCase("123423!#$%#$$%&%*(&*)54365567")]
    [TestCase("!%$%$#%&*&)()*+)(_$%$&*&*&()_")]
    public void Verify_SameValues_ReturnsTrue(string clearString)
    {
        var crypt = new Sha512Hasher();
        var crypted = crypt.Encrypt(clearString);

        Assert.IsTrue(crypt.Verify(clearString, crypted));
    }
    [Test]
    [TestCase("Lorem ipsum dolor sit amet,")]
    [TestCase("dfasdfa")]
    [TestCase("giammin")]
    [TestCase("12342354365567")]
    [TestCase("123423!#$%#$$%&%*(&*)54365567")]
    [TestCase("!%$%$#%&*&)()*+)(_$%$&*&*&()_")]
    public void Encrypt_SameValues_NotEqual(string clearString)
    {
        var crypt = new Sha512Hasher();
        var crypted = crypt.Encrypt(clearString);
        var decrypted = crypt.Encrypt(clearString);
        Assert.AreNotEqual(decrypted, clearString);
    }


}