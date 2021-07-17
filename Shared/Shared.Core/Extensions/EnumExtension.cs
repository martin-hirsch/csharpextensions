namespace Shared.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtensions
    {
        public static string ToStringByDescription(this Enum theEnum)
        {
            Type genericEnumType = theEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(theEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            return theEnum.ToString();
        }
    }
}