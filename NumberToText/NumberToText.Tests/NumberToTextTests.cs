using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static NumberToText.NumberToTextConverter;

namespace NumberToText.Tests
{
    [TestClass]
    public class NumberToTextTests
    {
        [TestMethod]
        public void RoundNumbers()
        {
            Assert.AreEqual("ნული", Convert(0));
            Assert.AreEqual("ერთი", Convert(1));
            Assert.AreEqual("ათი", Convert(10));
            Assert.AreEqual("ასი", Convert(100));
            Assert.AreEqual("ერთი ათასი", Convert(1000));
            Assert.AreEqual("ორი ათასი", Convert(2000));
            Assert.AreEqual("ათი ათასი", Convert(10000));
            Assert.AreEqual("ოცი ათასი", Convert(20000));
            Assert.AreEqual("ასი ათასი", Convert(100000));
            Assert.AreEqual("ორასი ათასი", Convert(200000));
            Assert.AreEqual("ერთი მილიონი", Convert(1000000));
            Assert.AreEqual("ორი მილიონი", Convert(2000000));
            Assert.AreEqual("ათი მილიონი", Convert(10000000));
            Assert.AreEqual("ასი მილიონი", Convert(100000000));
            Assert.AreEqual("ორასი მილიონი", Convert(200000000));
            Assert.AreEqual("ერთი მილიარდი", Convert(1000000000));
            Assert.AreEqual("ათი მილიარდი", Convert(10000000000));
            Assert.AreEqual("ასი მილიარდი", Convert(100000000000));
        }

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
            Assert.AreEqual("ოთხმოცდაცხრამეტი", Convert(99));
            Assert.AreEqual("თერთმეტი", Convert(11));
            Assert.AreEqual("ათი", Convert(10));
            Assert.AreEqual("ოცი", Convert(20));
            Assert.AreEqual("ოცდაერთი", Convert(21));
        }

        [TestMethod]
        public void ThreeDigitNumber()
        {
            Assert.AreEqual("ცხრაასი", Convert(900));
            Assert.AreEqual("ორასი", Convert(200));
            Assert.AreEqual("ასი", Convert(100));
            Assert.AreEqual("ას ერთი", Convert(101));
            Assert.AreEqual("ორას ოცდაერთი", Convert(221));
            Assert.AreEqual("ას ოცდათხუთმეტი", Convert(135));
            Assert.AreEqual("ას ათი", Convert(110));
            Assert.AreEqual("ცხრაას ორმოცდათხუთმეტი", Convert(955));
            Assert.AreEqual("ცხრაას ჩვიდმეტი", Convert(917));
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

        [TestMethod]
        public void SevenDigitNumber()
        {
            Assert.AreEqual("ერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(1322345));
        }

        [TestMethod]
        public void EightDigitNumber()
        {
            Assert.AreEqual("ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(21322345));
        }

        [TestMethod]
        public void NineDigitNumber()
        {
            Assert.AreEqual("სამას ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(321322345));

        }

        [TestMethod]
        public void TenDigitNumber()
        {
            Assert.AreEqual("შვიდი მილიარდ სამას ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(7321322345));

        }
    }
}
