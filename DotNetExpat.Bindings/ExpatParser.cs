using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Expat
{
    public class ExpatParser : IDisposable
    {
        private volatile bool disposed;
        protected string encoding;
        protected IntPtr parser;
        protected GCHandle userData;

        public bool IsDisposed => disposed;

        public ExpatParser(string encoding_ = default)
        {
            encoding = encoding_;
            parser = Native.Expat_Create(encoding);
        }

        public long CurrentLineNumber
        {
            get
            {
                ThrowIfDiposed();
                return Native.Expat_GetCurrentLineNumber(parser);
            }
        }

        public int ByteIndex
        {
            get
            {
                ThrowIfDiposed();
                return Native.Expat_GetCurrentByteIndex(parser);
            }
        }

        public int ByteCount
        {
            get
            {
                ThrowIfDiposed();
                return Native.Expat_GetCurrentByteCount(parser);
            }
        }

        public ExpatError GetLastErrror()
        {
            ThrowIfDiposed();
            return Native.Expat_GetErrorCode(parser);
        }

        ~ExpatParser()
        {
            GC.SuppressFinalize(this);
        }

        public ExpatStatus Parse(byte[] buffer, int count, bool final = true)
        {
            ThrowIfDiposed();

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try
            {
                return Native.Expat_Parse(parser, handle.AddrOfPinnedObject(), count, final);
            }
            finally
            {
                handle.Free();
            }
        }

        public IntPtr GetPointer()
            => parser;

        public bool Reset()
        {
            ThrowIfDiposed();
            return Native.Expat_Reset(parser, encoding);
        }

        public void SetUserData(object user_data)
        {
            ThrowIfDiposed();

            if (userData.AddrOfPinnedObject() != IntPtr.Zero)
                userData.Free();

            userData = GCHandle.Alloc(user_data, GCHandleType.Pinned);
            Native.Expat_SetUserData(parser, userData.AddrOfPinnedObject());

        }

        public void SetElementHandler(XML_StartElementHandler start, XML_EndElementHandler end)
        {
            ThrowIfDiposed();
            Native.Expat_SetElementHandler(parser, start, end);
        }

        protected void ThrowIfDiposed()
        {
            if (disposed)
                throw new ObjectDisposedException("Parser already disposed.");
        }

        public void Dispose()
        {
            ThrowIfDiposed();

            disposed = true;

            if (userData.AddrOfPinnedObject() != IntPtr.Zero)
                userData.Free();

            if (parser != IntPtr.Zero)
            {
                Native.Expat_Release(parser);
                parser = IntPtr.Zero;
            }
        }
    }
}
