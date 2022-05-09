namespace NaijaPidginAPI.Extentions
{
    public static class StringExceptions
    {
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s.Trim());
        }
    }
}
