namespace Expat
{
    public static class Utilities
    {
        public static string GetErrorDescription(ExpatError error)
            => Native.Expat_GetErrorDescription(error);
    }
}
