namespace Shared.Core.Converter
{
    using System;
    using System.Text.RegularExpressions;
    using Shared.Core.Extensions;

    public class StringToTimeSpanConverter
    {
        private readonly Regex _hoursRegex = new Regex(@"\d*\s*((h)|(H)|(hh)|(HH)|(ho)|(HO)|(hou)|(HOU)|(hour)|(HOUR)|(hours)|(HOURS))");

        private readonly Regex _minRegex = new Regex(@"\d*\s*((m)|(M)|(mi)|(MI)|(min)|(MIN)|(mins)|(MINS))");

        private readonly Regex _stdRegex = new Regex(@"\d*\s*((std.)|(Std.))");

        private readonly Regex _stundenRegex = new Regex(@"\d*\s*((s)|(S)|(St)|(ST)|(stu)|(STU)|(stun)|(STUN)|(stund)|(STUND)|(stunde)|(STUNDE)|(stunden)|(STUNDEN))");

        public TimeSpan? Convert(string input)
        {
            TimeSpan? result = null;
            if (input == null)
            {
                return null;
            }

            if (input == string.Empty)
            {
                return null;
            }

            var parsedHours = TryParseHours(input);
            var parsedMinutes = TryParseMinutes(input);

            if (parsedHours.HasValue)
            {
                result = parsedHours;
            }

            if (parsedMinutes.HasValue)
            {
                result = result?.Add(parsedMinutes.Value) ?? parsedMinutes;
            }

            return result;
        }

        private TimeSpan? TryParseHours(string input)
        {
            TimeSpan? result = null;
            if (_hoursRegex.IsMatch(input))
            {
                if (int.TryParse(_hoursRegex.Match(input).Value.GetAllDigits(), out var digits))
                {
                    result = TimeSpan.FromHours(digits);
                }
            }
            else if (_stdRegex.IsMatch(input))
            {
                if (int.TryParse(_stdRegex.Match(input).Value.GetAllDigits(), out var digits))
                {
                    result = TimeSpan.FromHours(digits);
                }
            }
            else if (_stundenRegex.IsMatch(input))
            {
                if (int.TryParse(_stundenRegex.Match(input).Value.GetAllDigits(), out var digits))
                {
                    result = TimeSpan.FromHours(digits);
                }
            }

            return result;
        }

        private TimeSpan? TryParseMinutes(string input)
        {
            TimeSpan? result = null;
            if (!_minRegex.IsMatch(input))
            {
                return result;
            }

            if (int.TryParse(_minRegex.Match(input).Value.GetAllDigits(), out var digits))
            {
                result = TimeSpan.FromMinutes(digits);
            }

            return result;
        }
    }
}