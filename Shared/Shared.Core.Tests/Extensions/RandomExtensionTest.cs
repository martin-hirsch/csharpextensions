using System;
using NUnit.Framework;
using Shared.Core.Extensions;

namespace Shared.Core.Tests.Extensions
{
    [TestFixture]
    public class RandomExtensionTest
    {
        [Test]
        public void RandomString_does_not_generate_the_same_string_twice()
        {
            var sut = new Random();
            var result1 = sut.RandomString(12);
            var result2 = sut.RandomString(12);

            Assert.That(result1, Is.Not.EqualTo(result2));
        }

        [Test]
        public void RandomString_generates_random_string_with_given_length()
        {
            var sut = new Random();
            var result = sut.RandomString(12);

            Assert.That(result.Length, Is.EqualTo(12));
        }
    }
}