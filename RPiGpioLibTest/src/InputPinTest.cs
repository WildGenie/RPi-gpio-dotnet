using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPi.Gpio;
using Moq;

namespace RPi.Gpio.Test
{
    [TestClass]
    public class InputPinTest
    {
        private Mock<IGpioAccess> gpioMock;
        private IGpioAccess gpio;

        [TestInitialize]
        public void Setup()
        {
            gpioMock = new Mock<IGpioAccess>();
            gpio = gpioMock.Object;
        }

        [TestMethod]
        public void ReadHigh()
        {
            gpioMock.Setup(g => g.ReadPin(4)).Returns(true);
            InputPin pin = new InputPin(4, gpio);
            pin.ReadLevel();
            Assert.IsTrue(pin.ReadLevel());
        }

        [TestMethod]
        public void ReadLow()
        {
            gpioMock.Setup(g => g.ReadPin(4)).Returns(false);
            InputPin pin = new InputPin(4, gpio);
            pin.ReadLevel();
            Assert.IsFalse(pin.ReadLevel());
        }
    }
}
