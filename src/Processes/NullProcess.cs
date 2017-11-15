﻿using System;
using System.Diagnostics;
using System.IO;
using NullGuard;

namespace Rothko
{
    public sealed class NullProcess : IProcess
    {
#pragma warning disable 67
        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;
#pragma warning restore 67

        public NullProcess(ProcessStartInfo psi)
        {
            StartInfo = psi;
        }

        public NullProcess()
        {
        }

        public int Id { get { return 4; } }

        public ProcessStartInfo StartInfo
        {
            [return: AllowNull]
            get; set;
        }

        public StreamWriter StandardInput
        {
            [return: AllowNull]
            get { return null; }
        }

        public StreamReader StandardOutput
        {
            [return: AllowNull]
            get { return null; }
        }

        public StreamReader StandardError
        {
            [return: AllowNull]
            get { return null; }
        }

        public int ExitCode { get { return 0; } }
        public IntPtr MainWindowHandle { get { return IntPtr.Zero; } }

        public string ProcessName
        {
            [return: AllowNull]
            get { return null; }
        }

        public void BeginOutputReadLine()
        {
        }

        public void BeginErrorReadLine()
        {
        }

        public bool Start()
        {
            return true;
        }

        public bool WaitForExit(int milliseconds)
        {
            return true;
        }

        public void Close()
        {
        }

        public void Kill()
        {
        }

        public void KillProcessTree()
        {
        }

        public bool Show()
        {
            return false;
        }

        public void Dispose()
        {
        }
    }
}