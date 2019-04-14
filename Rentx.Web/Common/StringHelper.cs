namespace Rentx.Web.Common
{
    public static class StringHelper
    {
        private const int DescriptionMaxLength = 70;
        private const int TitleMaxLength = 15;
        private const string TrimmedSymbol = "...";

        public static string TrimDescription(this string description)
        {
            var trimmedTitle = TrimText(description, DescriptionMaxLength);

            return trimmedTitle;
        }

        public static string TrimTitle(this string title)
        {
            var trimmedTitle = TrimText(title, TitleMaxLength);

            return trimmedTitle;
        }

        private static string TrimText(string text, int maxLength)
        {
            if (text.Length < maxLength)
            {
                return text;
            }

            return text.Substring(0, maxLength).Trim() + TrimmedSymbol;
        }
    }
}
