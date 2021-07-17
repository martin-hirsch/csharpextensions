namespace Shared.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Shared.Core.Helper;

    public static class StringExtension
    {
        public static string GetAllDigits(this string me)
        {
            var result = string.Empty;

            if (me == null)
            {
                return result;
            }

            if (RegexHelper.AllDigits.IsMatch(me) == false)
            {
                return result;
            }

            var matchList = (from Match match in RegexHelper.AllDigits.Matches(me) select match.Value).ToList();
            result = string.Concat(matchList.Select(x => x));
            
            return result;
        }

        public static IEnumerable<string> GetConsecutiveDigits(this string me)
        {
            if (me == null)
            {
                yield break;
            }

            if (RegexHelper.AllDigits.IsMatch(me) == false)
            {
                yield break;
            }

            var matches = RegexHelper.AllDigits.Matches(me);

            foreach (Match match in matches)
            {
                yield return match.Value;
            }
        }

        public static string ToReadableString(this IEnumerable<string> me)
        {
            if (me == null || !me.Any())
            {
                return string.Empty;
            }

            return me.Aggregate((x, y) => x + "," + y);
        }

        public static string ToPleasentlyReadableString(this IEnumerable<string> me)
        {
            if (me == null)
            {
                return string.Empty;
            }
            string[] strings = me.ToArray();
            if (strings.Length == 0)
            {
                return string.Empty;
            }
            if (strings.Length == 1)
            {
                return strings[0];
            }

            string result = string.Join(", ", strings, 0, strings.Length - 1);
            result += " und " + strings[strings.Length - 1];

            return result;
        }
    }
}