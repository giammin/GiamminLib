using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GiamminLib.IO
{
    public class FileUtility
    {
        public virtual void DeleteOlderFiles(string path, TimeSpan timeSpan, params string[] exclusions)
        {
            var toExclude = new HashSet<string>(exclusions, StringComparer.OrdinalIgnoreCase);
            foreach (string file in Directory.GetFiles(path))
            {
                var fi = new FileInfo(file);
                if (fi.LastWriteTime.ToUniversalTime().Add(timeSpan) < DateTime.UtcNow 
                    && ! toExclude.Contains(fi.Name))
                {
                    fi.Delete();
                }
            }
        }
        public static bool IsValidFileName(string fileName, IEnumerable<char>? fileNameNotAllowedChars = null)
        {
            var notAllowedChars = fileNameNotAllowedChars ?? Path.GetInvalidFileNameChars();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace.", nameof(fileName));
            }
            return fileName.IndexOfAny(notAllowedChars.ToArray()) == -1;
        }
    }
}
