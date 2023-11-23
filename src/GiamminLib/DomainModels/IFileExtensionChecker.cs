using System;
using System.Collections.Generic;
using System.IO;

namespace GiamminLib.DomainModels;

public interface IFileExtensionChecker
{
    /// <summary>
    /// check if file is of the allowed extensions
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="allowedExtensionsLowercase">lowercase extensions list. The value includes the period (".")</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">if filePath arg is null</exception>
    /// <exception cref="ArgumentNullException">if allowedExtensionsLowercase is null o empty</exception>
    bool IsValid(string filePath, IList<string> allowedExtensionsLowercase);

    bool IsValid(FileStream fileStream, IList<string> allowedExtensionsLowercase);
    bool IsValidSignature(Stream data, string extensionLowercase);
}