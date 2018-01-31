namespace DotNetToolkit.Wpf.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which the end in the current instance is replaced with another specified string.
        /// </summary>
        /// <param name="s">The source string.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <returns>The new string in which the end in the current instance is replaced with another specified string.</returns>
        public static string ReplaceEnd(this string s, string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(s) && s.EndsWith(oldValue))
            {
                var i = s.LastIndexOf(oldValue);
                return s.Remove(i, oldValue.Length).Insert(i, newValue);
            }

            return s;
        }
    }
}
