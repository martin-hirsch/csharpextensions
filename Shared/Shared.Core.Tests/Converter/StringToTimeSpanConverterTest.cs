using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shared.Core.Tests.Converter
{
    [TestFixture]
    public sealed partial class StringToTimeSpanConverterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(nameof(Convert_DataSource))]
        public void Convert_ShouldWork(string input, TimeSpan? expected)
        {
            var sut = Fixture.CreateTestObject();
            var result = sut.Convert(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> Convert_DataSource()
        {
            yield return new TestCaseData("10 mins", TimeSpan.FromMinutes(10));
            yield return new TestCaseData("5mins", TimeSpan.FromMinutes(5));
            yield return new TestCaseData("35 min", TimeSpan.FromMinutes(35));
            yield return new TestCaseData("ca. 10 Min.", TimeSpan.FromMinutes(10));
            yield return new TestCaseData("5 Minuten", TimeSpan.FromMinutes(5));
            yield return new TestCaseData("10 Min.", TimeSpan.FromMinutes(10));
            yield return new TestCaseData("10Min.", TimeSpan.FromMinutes(10));
            yield return new TestCaseData("6 Std.", TimeSpan.FromHours(6));
            yield return new TestCaseData("2 hr 45 min", TimeSpan.FromMinutes(165));
            yield return new TestCaseData("0 h 30 min", TimeSpan.FromMinutes(30));
            yield return new TestCaseData("100 min", TimeSpan.FromMinutes(100));
            yield return new TestCaseData("1 hour 45 minutes", TimeSpan.FromMinutes(105));
            yield return new TestCaseData("6 hours", TimeSpan.FromHours(6));
            yield return new TestCaseData("2 hr 10 min", TimeSpan.FromMinutes(130));
            yield return new TestCaseData("2 h", TimeSpan.FromMinutes(120));
            yield return new TestCaseData("5 min", TimeSpan.FromMinutes(5));
            yield return new TestCaseData("2 h 30 Min", TimeSpan.FromMinutes(150));
            yield return new TestCaseData("1 hour", TimeSpan.FromMinutes(60));
            yield return new TestCaseData("30 Min", TimeSpan.FromMinutes(30));
            yield return new TestCaseData("30 - 45Min.", TimeSpan.FromMinutes(45));
            yield return new TestCaseData("1 hr 45 min", TimeSpan.FromMinutes(105));
            yield return new TestCaseData("1 h 10 min", TimeSpan.FromMinutes(70));
            yield return new TestCaseData("25 min", TimeSpan.FromMinutes(25));
            yield return new TestCaseData("20 Minuten", TimeSpan.FromMinutes(20));
            yield return new TestCaseData("10 minuten", TimeSpan.FromMinutes(10));
            yield return new TestCaseData("10 Min", TimeSpan.FromMinutes(10));
            yield return new TestCaseData("1 hr", TimeSpan.FromMinutes(60));
            yield return new TestCaseData("0 Min.", TimeSpan.FromMinutes(0));
            yield return new TestCaseData("3 hr 30 min", TimeSpan.FromMinutes(210));
            yield return new TestCaseData("5", null);
            yield return new TestCaseData("ca 20 Min.", TimeSpan.FromMinutes(20));
            yield return new TestCaseData("5 minuten", TimeSpan.FromMinutes(5));
            yield return new TestCaseData("3 hr", TimeSpan.FromMinutes(180));
            yield return new TestCaseData("10 Minuten", TimeSpan.FromMinutes(10));
            yield return new TestCaseData(string.Empty, null);
            yield return new TestCaseData("    ", null);
            yield return new TestCaseData(null, null);
        }
    }
}