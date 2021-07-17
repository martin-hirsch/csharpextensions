using Shared.Core.Converter;

namespace Shared.Core.Tests.Converter
{
    public sealed partial class StringToTimeSpanConverterTest
    {
        private class Fixture
        {
            public static StringToTimeSpanConverter CreateTestObject()
            {
                return new StringToTimeSpanConverter();
            }
        }
    }
}