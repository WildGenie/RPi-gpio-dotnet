using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RPi.Gpio.Test
{
    [TestClass]
    public class GpioPinsRev1Test
    {
        private GpioPinsRev1 pins;

        [TestInitialize]
        public void Setup()
        {
            pins = new GpioPinsRev1();
        }

        [TestMethod]
        public void HasUsablePinNumber()
        {
            Assert.IsFalse(pins.HasUsablePinNumber(0));
            Assert.IsFalse(pins.HasUsablePinNumber(1));
            Assert.IsFalse(pins.HasUsablePinNumber(2));
            Assert.IsTrue(pins.HasUsablePinNumber(3));
            Assert.IsFalse(pins.HasUsablePinNumber(4));
            Assert.IsTrue(pins.HasUsablePinNumber(5));
            Assert.IsFalse(pins.HasUsablePinNumber(6));
            Assert.IsTrue(pins.HasUsablePinNumber(7));
            Assert.IsTrue(pins.HasUsablePinNumber(8));
            Assert.IsFalse(pins.HasUsablePinNumber(9));
            Assert.IsTrue(pins.HasUsablePinNumber(10));
            Assert.IsTrue(pins.HasUsablePinNumber(11));
            Assert.IsTrue(pins.HasUsablePinNumber(12));
            Assert.IsTrue(pins.HasUsablePinNumber(13));
            Assert.IsFalse(pins.HasUsablePinNumber(14));
            Assert.IsTrue(pins.HasUsablePinNumber(15));
            Assert.IsTrue(pins.HasUsablePinNumber(16));
            Assert.IsFalse(pins.HasUsablePinNumber(17));
            Assert.IsTrue(pins.HasUsablePinNumber(18));
            Assert.IsTrue(pins.HasUsablePinNumber(19));
            Assert.IsFalse(pins.HasUsablePinNumber(20));
            Assert.IsTrue(pins.HasUsablePinNumber(21));
            Assert.IsTrue(pins.HasUsablePinNumber(22));
            Assert.IsTrue(pins.HasUsablePinNumber(23));
            Assert.IsTrue(pins.HasUsablePinNumber(24));
            Assert.IsFalse(pins.HasUsablePinNumber(25));
            Assert.IsTrue(pins.HasUsablePinNumber(26));
            Assert.IsFalse(pins.HasUsablePinNumber(27));
            Assert.IsFalse(pins.HasUsablePinNumber(28));
            Assert.IsFalse(pins.HasUsablePinNumber(29));
        }

        [TestMethod]
        public void IdOfPinNumber()
        {
            Assert.AreEqual<int>(0, pins.IdOfPinNumber(3));
            Assert.AreEqual<int>(1, pins.IdOfPinNumber(5));
            Assert.AreEqual<int>(4, pins.IdOfPinNumber(7));
            Assert.AreEqual<int>(14, pins.IdOfPinNumber(8));
            Assert.AreEqual<int>(15, pins.IdOfPinNumber(10));
            Assert.AreEqual<int>(17, pins.IdOfPinNumber(11));
            Assert.AreEqual<int>(18, pins.IdOfPinNumber(12));
            Assert.AreEqual<int>(21, pins.IdOfPinNumber(13));
            Assert.AreEqual<int>(22, pins.IdOfPinNumber(15));
            Assert.AreEqual<int>(23, pins.IdOfPinNumber(16));
            Assert.AreEqual<int>(24, pins.IdOfPinNumber(18));
            Assert.AreEqual<int>(10, pins.IdOfPinNumber(19));
            Assert.AreEqual<int>(9, pins.IdOfPinNumber(21));
            Assert.AreEqual<int>(25, pins.IdOfPinNumber(22));
            Assert.AreEqual<int>(11, pins.IdOfPinNumber(23));
            Assert.AreEqual<int>(8, pins.IdOfPinNumber(24));
            Assert.AreEqual<int>(7, pins.IdOfPinNumber(26));
        }
    }
}
