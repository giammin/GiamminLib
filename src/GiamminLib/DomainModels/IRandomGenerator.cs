namespace GiamminLib.DomainModels;

public interface IRandomGenerator
{
    /// <summary>
    /// Generates a random string with specified string length.
    /// </summary>
    /// <param name="stringLength">Length of the string to generate.</param>
    /// <param name="useLowerCase">if set to <c>true</c> use lower case chars.</param>
    /// <param name="useUpperCase">if set to <c>true</c> use upper case chars.</param>
    /// <param name="useNumbers">if set to <c>true</c> use numbers chars.</param>
    /// <param name="useSymbols">if set to <c>true</c> use symbols chars.</param>
    /// <returns></returns>
    public string GenerateString(int stringLength, bool useLowerCase = true, bool useUpperCase = false,
        bool useNumbers = true, bool useSymbols = false);

}