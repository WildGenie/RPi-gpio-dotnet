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
    public class PollingEventInputPinTest
    {
        private Mock<IGpioAccess> gpioMock;
        private IGpioAccess gpio;

        [TestInitialize]
        public void Setup()
        {
            gpioMock = new Mock<IGpioAccess>();
            gpioMock.Setup(g => g.ReadPinEvent(4)).Returns(true);
            gpio = gpioMock.Object;
        }

        [TestMethod]
        public void Tick()
        {
            HashSet<InputPinEventType> types = new HashSet<InputPinEventType>();
            types.Add(InputPinEventType.LevelLow);
            FakeTimer timer = new FakeTimer();

            bool isDelegateCalled = false;
            PollingEventInputPin pin = new PollingEventInputPin(4, gpio, types, timer);
            pin.Pin += new EventHandler((object sender, EventArgs args) => { isDelegateCalled = true; });
            timer.SimulateTick();
            Assert.IsTrue(isDelegateCalled);
            gpioMock.Verify(g => g.ReadPinEvent(4));
            gpioMock.Verify(g => g.ClearPinEvent(4));
        }
    }
}
