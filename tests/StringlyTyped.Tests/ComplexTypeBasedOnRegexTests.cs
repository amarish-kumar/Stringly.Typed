using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace StringlyTyped.Tests
{
    [TestFixture]
    public class ComplexTypeBasedOnRegexTests
    {
        [Test]
        public void Ctor_ValueMatchesRegex_CreatesComplexType()
        {
            DigitsOnly stringly = new Stringly<DigitsOnly>("0000");

            Assert.That(stringly.Digits.Count, Is.EqualTo(4));
        }

        [Test]
        public void Ctor_ValueDoesNotMatchRegex_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Stringly<DigitsOnly>("word"));
        }
    }

    internal class DigitsOnly : StringlyPattern<DigitsOnly>
    {
        protected override Regex Regex => new Regex(@"^\d+$");

        public List<int> Digits { get; private set; }

        protected override DigitsOnly ParseFromRegex(Match match)
        {
            var digits = match.Value.ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToList();

            return new DigitsOnly { Digits = digits };
        }
    }
}