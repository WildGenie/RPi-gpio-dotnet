using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPi.Gpio;
using Moq;
using RPi.Gpio.Timers;

namespace RPi.Gpio.Test
{
    [TestClass]
    public class GpioConfigTest
    {
        private GpioConfig config;
        private Mock<IGpioAccess> gpioMock;
        private IGpioAccess gpio;
        private ITimer timer;

        [TestInitialize]
        public void Setup()
        {
            gpioMock = new Mock<IGpioAccess>();
            gpio = gpioMock.Object;
            config = new GpioConfig(gpio);
            timer = new FakeTimer();
        }

        [TestMethod]
        public void InputPin()
        {
            InputPin pin1 = config.InputPin(1);
            Assert.IsNotNull(pin1);
        }

        [TestMethod]
        public void OutputPin()
        {
            OutputPin pin2 = config.OutputPin(2);
            Assert.IsNotNull(pin2);
        }

        [TestMethod]
        public void PollingEventInputPin()
        {
            PollingEventInputPin pin2 = config.PollingEventInputPin(2, timer, InputPinEventType.FallingEdge);
            Assert.IsNotNull(pin2);
        }
    }
}
