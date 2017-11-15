﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using NullGuard;

namespace Rothko
{
    [Export(typeof(DirectoryFacade))]
    public class DirectoryFacade : IDirectoryFacade
    {
        public IDirectoryInfo CreateDirectory(string path)
        {
            return DirectoryInfo.Wrap(Directory.CreateDirectory(path));
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
        
        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            return Directory.EnumerateDirectories(path, searchPattern);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }

        public IEnumerable<string> EnumerateFiles(string path)
        {
            return EnumerateFiles(path, "*.*");
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        [return: AllowNull]
        public IDirectoryInfo GetParent(string path)
        {
            return DirectoryInfo.Wrap(Directory.GetParent(path));
        }

        public IDirectoryInfo GetDirectory(string path)
        {
            return new DirectoryInfo(path);
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public bool IsEmpty(string path)
        {
            var directory = GetDirectory(path);
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException(
                    String.Format(CultureInfo.InvariantCulture, "The directory '{0}' does not exist", path));
            }

            return directory.IsEmpty;
        }
    }
}
