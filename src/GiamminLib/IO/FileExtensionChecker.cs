using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GiamminLib.DomainModels;

namespace GiamminLib.IO;

public class FileExtensionChecker : IFileExtensionChecker
{
    private readonly bool _throwExceptionIfSignatureMissing;
    private readonly IReadOnlyDictionary<string, List<byte[]>> _signatures;

    public FileExtensionChecker(bool throwExceptionIfSignatureMissing = true, IReadOnlyDictionary<string, List<byte[]>>? customSignatures = null)
    {
        _throwExceptionIfSignatureMissing = throwExceptionIfSignatureMissing;
        _signatures = customSignatures ?? Constants.FileSignatures;
    }
    /// <summary>
    /// check if a file is of the allowed extensions
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="allowedExtensionsLowercase">lowercase extensions list. The value includes the period (".")</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">if filePath arg is null</exception>
    /// <exception cref="ArgumentNullException">if allowedExtensionsLowercase is null o empty</exception>
    public bool IsValid(string filePath, IList<string> allowedExtensionsLowercase)
    {
        bool rtn;
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException("file does not exist",filePath);
        }
        using var fs = fileInfo.OpenRead();
        rtn = IsValid(fs, allowedExtensionsLowercase);
        return rtn;
    }
    public bool IsValid(FileStream fileStream, IList<string> allowedExtensionsLowercase)
    {
        bool rtn = false;
        if (allowedExtensionsLowercase == null || allowedExtensionsLowercase.Count < 1)
        {
            throw new ArgumentNullException(nameof(allowedExtensionsLowercase));
        }
        var ext = Path.GetExtension(fileStream.Name).ToLowerInvariant();

        if (allowedExtensionsLowercase.Contains(ext))
        {
            rtn = IsValidSignature(fileStream, ext);
        }
        return rtn;
    }

    public bool IsValidSignature(Stream data, string extensionLowercase)
    {
        bool rtn;
        if (data == null || data.Length == 0)
        {
            throw new ArgumentNullException(nameof(data));
        }
        if (_throwExceptionIfSignatureMissing && !_signatures.ContainsKey(extensionLowercase))
        {
            throw new ArgumentOutOfRangeException(nameof(extensionLowercase), $"signature for extension {extensionLowercase} not available");
        }

        data.Position = 0;

        using var reader = new BinaryReader(data);
        var signatures = _signatures[extensionLowercase];
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
        rtn = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
        return rtn;
    }
}