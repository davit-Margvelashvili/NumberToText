using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static NumberToText.NumberToTextConverter;

namespace NumberToText.Tests
{
    [TestClass]
    public class NumberToTextTests
    {
        [TestMethod]
        public void OneDigitNumber()
        {
            var expected = "ორი";
            var actual = Convert(2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TwoDigitNumber()
        {
            var expected = "ოცი";
            var actual = Convert(20);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThreeDigitNumber()
        {
            var expected = "ორას ოცდაერთი";
            var actual = Convert(221);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FourDigitNumber()
        {
            var expected = "ორი ათას სამას ორმოცდახუთი";
            var actual = Convert(2345);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FiveDigitNumber()
        {
            var expected = "ოცდაორი ათას სამას ორმოცდახუთი";
            var actual = Convert(22345);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SixDigitNumber()
        {
            var expected = "სამას ოცდაორი ათას სამას ორმოცდახუთი";
            var actual = Convert(322345);

            Assert.AreEqual(expected, actual);
        }
    }
}
