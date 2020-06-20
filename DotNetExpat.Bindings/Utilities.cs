using System.Runtime.InteropServices;

namespace Expat
{
    public static class Utilities
    {
        public static string GetDescription(this ExpatError error)
        {
            var ptr = Native.Expat_GetErrorDescription(error);
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}
