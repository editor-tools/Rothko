﻿using System;

namespace Rothko
{
    public abstract class FileSystemInfo : IFileSystemInfo
    {
        readonly System.IO.FileSystemInfo inner;

        protected FileSystemInfo(System.IO.FileSystemInfo inner)
        {
            this.inner = inner;
        }

        internal static IFileSystemInfo Wrap(System.IO.FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo == null) return null;
            if ((fileSystemInfo.Attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
            {
                return DirectoryInfo.Wrap((System.IO.DirectoryInfo)fileSystemInfo);
            }

            return FileInfo.Wrap((System.IO.FileInfo)fileSystemInfo);
        }

        public System.IO.FileAttributes Attributes
        {
            get
            {
                return inner.Attributes;
            }
            set
            {
                inner.Attributes = value;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                return inner.CreationTime;
            }
            set
            {
                inner.CreationTime = value;
            }
        }

        public DateTime CreationTimeUtc
        {
            get
            {
                return inner.CreationTimeUtc;
            }
            set
            {
                inner.CreationTimeUtc = value;
            }
        }

        public bool Exists
        {
            get { return inner.Exists; }
        }

        public string Extension
        {
            get { return inner.Extension; }
        }

        public string FullName
        {
            get { return inner.FullName; }
        }

        public DateTime LastAccessTime
        {
            get
            {
                return inner.LastAccessTime;
            }
            set
            {
                inner.LastAccessTime = value;
            }
        }

        public DateTime LastAccessTimeUtc
        {
            get
            {
                return inner.LastAccessTimeUtc;
            }
            set
            {
                inner.LastAccessTimeUtc = value;
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                return inner.LastWriteTime;
            }
            set
            {
                inner.LastWriteTime = value;
            }
        }

        public DateTime LastWriteTimeUtc
        {
            get
            {
                return inner.LastWriteTimeUtc;
            }
            set
            {
                inner.LastWriteTimeUtc = value;
            }
        }

        public string Name
        {
            get { return inner.Name; }
        }

        public void Delete()
        {
            inner.Delete();
        }

        public void Refresh()
        {
            inner.Refresh();
        }

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            inner.GetObjectData(info, context);
        }
    }
}
