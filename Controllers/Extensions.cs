using System;

public static class Extensions
{
    public static bool ContainsCaseInsensitive(this string s, string txt)
    {
    	if(s == null) return false;
        return s.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}