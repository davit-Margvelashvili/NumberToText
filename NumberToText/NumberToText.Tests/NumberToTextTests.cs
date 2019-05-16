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
            Assert.AreEqual("ნული", Convert(0));
            Assert.AreEqual("ერთი", Convert(1));
            Assert.AreEqual("ორი", Convert(2));

        }

        [TestMethod]
        public void TwoDigitNumber()
        {
            Assert.AreEqual("ოცდათერთმეტი", Convert(31));
            Assert.AreEqual("თერთმეტი", Convert(11));
            Assert.AreEqual("ათი", Convert(10));
            Assert.AreEqual("ოცი", Convert(20));
            Assert.AreEqual("ოცდაერთი", Convert(21));
        }

        [TestMethod]
        public void ThreeDigitNumber()
        {
            Assert.AreEqual("ორას ოცდაერთი", Convert(221));
            Assert.AreEqual("ას ოცდათხუთმეტი", Convert(135));
            Assert.AreEqual("ას ათი", Convert(110));
            Assert.AreEqual("ცხრაას ორმოცდათხუთმეტი", Convert(955));
        }

        [TestMethod]
        public void FourDigitNumber()
        {
            Assert.AreEqual("ორი ათას სამას ორმოცდახუთი", Convert(2345));
        }

        [TestMethod]
        public void FiveDigitNumber()
        {
            Assert.AreEqual("ოცდაორი ათას სამას ორმოცდახუთი", Convert(22345));
        }

        [TestMethod]
        public void SixDigitNumber()
        {
            Assert.AreEqual("სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(322345));
        }
    }
}
