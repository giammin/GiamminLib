using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GiamminLib.IO
{
    public class FileExtensionChecker
    {
        private readonly bool _throwExceptionIfSignatureMissing;
        private readonly IReadOnlyDictionary<string, List<byte[]>> _signatures;

        public FileExtensionChecker(bool throwExceptionIfSignatureMissing = true, Dictionary<string, List<byte[]>>? customSignatures = null)
        {
            _throwExceptionIfSignatureMissing = throwExceptionIfSignatureMissing;
            _signatures = customSignatures ?? Constants.FileSignatures;
        }
        /// <summary>
        /// verifica se l'estensione è nel range indicato e se effettivamente il file è di quel tipo
        /// </summary>
        /// <param name="fileName">percorso file</param>
        /// <param name="allowedExtensionsLowercase"> elenco delle estensioni accettate, in lowercase con punto iniziale</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"> se filename è nullo</exception>
        /// <exception cref="ArgumentNullException">se la lista delle estensioni supportate è nulla o vuota</exception>
        public bool IsValid(string fileName, IList<string> allowedExtensionsLowercase)
        {
            bool rtn = false;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace.", nameof(fileName));
            }
            var fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                using var fs = fileInfo.OpenRead();
                rtn = IsValid(fs, allowedExtensionsLowercase);
            }
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
            bool rtn = false;
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (_throwExceptionIfSignatureMissing && !_signatures.ContainsKey(extensionLowercase))
            {
                throw new ArgumentOutOfRangeException(nameof(extensionLowercase), $"signature for extension {extensionLowercase} not available");
            }

            data.Position = 0;

            using (var reader = new BinaryReader(data))
            {
                var signatures = _signatures[extensionLowercase];
                var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
                rtn = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
            return rtn;
        }
    }
}
