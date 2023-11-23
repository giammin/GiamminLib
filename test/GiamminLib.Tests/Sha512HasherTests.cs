using GiamminLib.Security.Cryptography;
using Xunit;

namespace GiamminLib.Tests;

public class Sha512HasherTests
{
    [Theory]
    [InlineData("Lorem ipsum dolor sit amet,")]
    [InlineData("dfasdfa")]
    [InlineData("giammin")]
    [InlineData("12342354365567")]
    [InlineData("123423!#$%#$$%&%*(&*)54365567")]
    [InlineData("!%$%$#%&*&)()*+)(_$%$&*&*&()_")]
    public void Verify_SameValues_ReturnsTrue(string clearString)
    {
        var crypt = new Sha512Hasher();
        var crypted = crypt.Encrypt(clearString);

        Assert.True(crypt.Verify(clearString, crypted));
    }
    [Theory]
    [InlineData("Lorem ipsum dolor sit amet,")]
    [InlineData("dfasdfa")]
    [InlineData("giammin")]
    [InlineData("12342354365567")]
    [InlineData("123423!#$%#$$%&%*(&*)54365567")]
    [InlineData("!%$%$#%&*&)()*+)(_$%$&*&*&()_")]
    public void Encrypt_SameValues_NotEqual(string clearString)
    {
        var crypt = new Sha512Hasher();
        var crypted = crypt.Encrypt(clearString);
        Assert.NotEqual(clearString, crypted);
    }


}