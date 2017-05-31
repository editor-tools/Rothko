﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Rothko
{
    [Export(typeof(IFileFacade))]
    public class FileFacade : IFileFacade
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public IFileInfo GetFile(string path)
        {
            return FileInfo.Wrap(new System.IO.FileInfo(path));
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        public void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        public IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            return File.ReadLines(path, encoding);
        }

        public string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        public async Task<byte[]> ReadAllBytesAsync(string path)
        {
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                var buffer = new MemoryStream();
                await file.CopyToAsync(buffer);
                return buffer.ToArray();
            }
        }

        public StreamReader OpenText(string path)
        {
            return File.OpenText(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }
    }
}
