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
    public class OutputPinTest
    {
        private Mock<IGpioAccess> gpioMock;
        private IGpioAccess gpio;

        [TestInitialize]
        public void Setup()
        {
            gpioMock = new Mock<IGpioAccess>();
            gpio = gpioMock.Object;
            gpioMock.Setup(g => g.ReadPin(4)).Returns(true);
        }

        [TestMethod]
        public void SetAndClear()
        {
            OutputPin pin = new OutputPin(4, gpio);
            gpioMock.Setup(g => g.ReadPin(4)).Returns(true);
            pin.Set();
            gpioMock.Verify(g => g.SetPin(4));
            Assert.IsTrue(pin.IsSet());
            gpioMock.Setup(g => g.ReadPin(4)).Returns(false);
            pin.Clear();
            gpioMock.Verify(g => g.ClearPin(4));
            Assert.IsFalse(pin.IsSet());
        }
    }
}
