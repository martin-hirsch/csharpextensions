using System.Collections.Generic;
using NUnit.Framework;
using Shared.Core.Extensions;

namespace Shared.Core.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionTest
    {
        [TestCaseSource(nameof(GetAllDigits_DataSource))]
        public void GetAllDigits_ShouldWork(string input, string expected)
        {
            var sut = input;

            var result = sut.GetAllDigits();

            Assert.That(result, Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> GetAllDigits_DataSource()
        {
            yield return new TestCaseData("abc", string.Empty);
            yield return new TestCaseData("123", "123");
            yield return new TestCaseData("abc123def", "123");
            yield return new TestCaseData("a1b2c3d", "123");
            yield return new TestCaseData("aa1bb2cc3dd", "123");
            yield return new TestCaseData("aa12bb3cc", "123");
            yield return new TestCaseData("aa1bb23cc", "123");
            yield return new TestCaseData("1abc2", "12");
            yield return new TestCaseData(null, string.Empty);
            yield return new TestCaseData(string.Empty, string.Empty);
        }

        [TestCaseSource(nameof(GetConsecutiveDigits_DataSource))]
        public void GetConsecutiveDigits_ShouldWork(string input, IEnumerable<string> expected)
        {
            var sut = input;
            var result = sut.GetConsecutiveDigits();

            Assert.AreEqual(result, expected);
        }

        private static IEnumerable<TestCaseData> GetConsecutiveDigits_DataSource()
        {
            yield return new TestCaseData("123", new List<string> { "123" });
            yield return new TestCaseData("a1b2c3", new List<string> { "1", "2", "3" });
            yield return new TestCaseData("abc123def", new List<string> { "123" });
        }
    }
}