using System;
using System.Runtime.InteropServices;

namespace SynchronizationMethods
{
    public class OsHandle : IDisposable
    {
        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        private bool _disposed = false;
        public IntPtr Handle { get; set; }

        public OsHandle()
        {
            Handle = IntPtr.Zero;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (!_disposed && Handle != IntPtr.Zero)
                {
                    CloseHandle(Handle);
                    Handle = IntPtr.Zero;
                }

                _disposed = true;
            }
        }

        ~OsHandle()
        {
            Dispose(false);
        }
    }
}