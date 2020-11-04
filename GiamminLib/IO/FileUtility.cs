using System;
using System.Collections.Generic;
using System.IO;

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
    }
}
