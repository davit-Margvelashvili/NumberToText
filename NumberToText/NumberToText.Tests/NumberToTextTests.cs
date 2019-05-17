using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static NumberToText.NumberToTextConverter;

namespace NumberToText.Tests
{
    [TestClass]
    public class NumberToTextTests
    {
        [TestMethod]
        public void DecimalNumbers()
        {
            var (whole, @decimal) = Convert(1234.37);
            Assert.AreEqual("ერთი ათას ორას ოცდათოთხმეტი", whole);
            Assert.AreEqual("ოცდაჩვიდმეტი", @decimal);
        }

        [TestMethod]
        public void RoundNumbers()
        {
            Assert.AreEqual("ნული", Convert(0).wholePart);
            Assert.AreEqual("ერთი", Convert(1).wholePart);
            Assert.AreEqual("ათი", Convert(10).wholePart);
            Assert.AreEqual("ასი", Convert(100).wholePart);
            Assert.AreEqual("ერთი ათასი", Convert(1000).wholePart);
            Assert.AreEqual("ორი ათასი", Convert(2000).wholePart);
            Assert.AreEqual("ათი ათასი", Convert(10000).wholePart);
            Assert.AreEqual("ოცი ათასი", Convert(20000).wholePart);
            Assert.AreEqual("ასი ათასი", Convert(100000).wholePart);
            Assert.AreEqual("ორასი ათასი", Convert(200000).wholePart);
            Assert.AreEqual("ერთი მილიონი", Convert(1000000).wholePart);
            Assert.AreEqual("ორი მილიონი", Convert(2000000).wholePart);
            Assert.AreEqual("ათი მილიონი", Convert(10000000).wholePart);
            Assert.AreEqual("ასი მილიონი", Convert(100000000).wholePart);
            Assert.AreEqual("ორასი მილიონი", Convert(200000000).wholePart);
            Assert.AreEqual("ერთი მილიარდი", Convert(1000000000).wholePart);
            Assert.AreEqual("ათი მილიარდი", Convert(10000000000).wholePart);
            Assert.AreEqual("ასი მილიარდი", Convert(100000000000).wholePart);
        }

        [TestMethod]
        public void OneDigitNumber()
        {

            Assert.AreEqual("ნული", Convert(0).wholePart);
            Assert.AreEqual("ერთი", Convert(1).wholePart);
            Assert.AreEqual("ორი", Convert(2).wholePart);
        }

        [TestMethod]
        public void TwoDigitNumber()
        {
            Assert.AreEqual("ოცდათერთმეტი", Convert(31).wholePart);
            Assert.AreEqual("ოთხმოცდაცხრამეტი", Convert(99).wholePart);
            Assert.AreEqual("თერთმეტი", Convert(11).wholePart);
            Assert.AreEqual("ათი", Convert(10).wholePart);
            Assert.AreEqual("ოცი", Convert(20).wholePart);
            Assert.AreEqual("ოცდაერთი", Convert(21).wholePart);
        }

        [TestMethod]
        public void ThreeDigitNumber()
        {
            Assert.AreEqual("ცხრაასი", Convert(900).wholePart);
            Assert.AreEqual("ორასი", Convert(200).wholePart);
            Assert.AreEqual("ასი", Convert(100).wholePart);
            Assert.AreEqual("ას ერთი", Convert(101).wholePart);
            Assert.AreEqual("ორას ოცდაერთი", Convert(221).wholePart);
            Assert.AreEqual("ას ოცდათხუთმეტი", Convert(135).wholePart);
            Assert.AreEqual("ას ათი", Convert(110).wholePart);
            Assert.AreEqual("ცხრაას ორმოცდათხუთმეტი", Convert(955).wholePart);
            Assert.AreEqual("ცხრაას ჩვიდმეტი", Convert(917).wholePart);
        }

        [TestMethod]
        public void FourDigitNumber()
        {
            Assert.AreEqual("ორი ათას სამას ორმოცდახუთი", Convert(2345).wholePart);
        }

        [TestMethod]
        public void FiveDigitNumber()
        {
            Assert.AreEqual("ოცდაორი ათას სამას ორმოცდახუთი", Convert(22345).wholePart);
        }

        [TestMethod]
        public void SixDigitNumber()
        {
            Assert.AreEqual("სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(322345).wholePart);
        }

        [TestMethod]
        public void SevenDigitNumber()
        {
            Assert.AreEqual("ერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(1322345).wholePart);
        }

        [TestMethod]
        public void EightDigitNumber()
        {
            Assert.AreEqual("ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(21322345).wholePart);
        }

        [TestMethod]
        public void NineDigitNumber()
        {
            Assert.AreEqual("სამას ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(321322345).wholePart);
        }

        [TestMethod]
        public void TenDigitNumber()
        {
            Assert.AreEqual("შვიდი მილიარდ სამას ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი", Convert(7321322345).wholePart);
        }
    }
}
