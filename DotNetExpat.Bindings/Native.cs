using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Expat
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void XML_StartElementHandler(IntPtr hUserData, string name, string[] attributes);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void XML_EndElementHandler(IntPtr hUserData, string name);

    internal static class Native
    {
        public const string LibraryName = "DotNetExpat";

        [DllImport(LibraryName)]
        public static extern IntPtr Expat_Create(string encoding = default);

        [DllImport(LibraryName)]
        public static extern void Expat_Release(IntPtr hParser);

        [DllImport(LibraryName)]
        public static extern bool Expat_Reset(IntPtr hParser, string encoding = default);

        [DllImport(LibraryName)]
        public static extern ExpatStatus Expat_Parse(IntPtr hParser, IntPtr hData, int count, bool end);

        [DllImport(LibraryName)]
        public static extern StringBuilder Expat_GetErrorDescription(ExpatError error);

        [DllImport(LibraryName)]
        public static extern ExpatError Expat_GetErrorCode(IntPtr hParser);

        [DllImport(LibraryName)]
        public static extern long Expat_GetCurrentLineNumber(IntPtr hParser);

        [DllImport(LibraryName)]
        public static extern int Expat_GetCurrentByteIndex(IntPtr hParser);

        [DllImport(LibraryName)]
        public static extern int Expat_GetCurrentByteCount(IntPtr hParser);

        [DllImport(LibraryName)]
        public static extern void Expat_SetUserData(IntPtr hParser, IntPtr hUserData);

        [DllImport(LibraryName)]
        public static extern void Expat_SetElementHandler(IntPtr hParser,
            XML_StartElementHandler startElementHandler,
            XML_EndElementHandler endElementHandler);
    }
}
