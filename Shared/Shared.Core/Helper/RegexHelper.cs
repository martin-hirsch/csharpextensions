namespace Shared.Core.Helper
{
    using System.Text.RegularExpressions;

    public static class RegexHelper
    {
        public static Regex NonDigit => new Regex(@"[^\d]+");

        public static Regex AllDigits = new Regex(@"[\d]+");
    }
}