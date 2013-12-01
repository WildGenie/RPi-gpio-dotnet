using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RPi.Gpio.Test
{
    [TestClass]
    public class EventInputPinTest
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
        public void Dispose()
        {
            HashSet<InputPinEventType> types = new HashSet<InputPinEventType>();
            types.Add(InputPinEventType.LevelLow);
            EventInputPin pin = new StubEventInputPin(4, gpio, types);
            pin.Dispose();
            gpioMock.Verify(g => g.ConfigLevelLowDetection(4, false));

            types.Add(InputPinEventType.LevelHigh);
            pin = new StubEventInputPin(4, gpio, types);
            pin.Dispose();
            gpioMock.Verify(g => g.ConfigLevelLowDetection(4, false));
            gpioMock.Verify(g => g.ConfigLevelHighDetection(4, false));

            types.Add(InputPinEventType.FallingEdge);
            pin = new StubEventInputPin(4, gpio, types);
            pin.Dispose();
            gpioMock.Verify(g => g.ConfigLevelLowDetection(4, false));
            gpioMock.Verify(g => g.ConfigLevelHighDetection(4, false));
            gpioMock.Verify(g => g.ConfigFallingEdgeDetection(4, false));

            types.Add(InputPinEventType.RisingEdge);
            pin = new StubEventInputPin(4, gpio, types);
            pin.Dispose();
            gpioMock.Verify(g => g.ConfigLevelLowDetection(4, false));
            gpioMock.Verify(g => g.ConfigLevelHighDetection(4, false));
            gpioMock.Verify(g => g.ConfigFallingEdgeDetection(4, false));
            gpioMock.Verify(g => g.ConfigRisingEdgeDetection(4, false));
        }

        [TestMethod]
        public void CallbackInvocation()
        {
            HashSet<InputPinEventType> types = new HashSet<InputPinEventType>();
            types.Add(InputPinEventType.LevelLow);

            bool isDelegateCalled = false;
            StubEventInputPin pin = new StubEventInputPin(4, gpio, types);
            pin.Pin += new EventHandler((object sender, EventArgs args) => { isDelegateCalled = true; });
            pin.SimulateEvent();
            Assert.IsTrue(isDelegateCalled);
        }

        private sealed class StubEventInputPin : EventInputPin
        {
            public StubEventInputPin(int pinId, IGpioAccess gpioAccess, ISet<InputPinEventType> types)
                : base(pinId, gpioAccess, types)
            {
            }

            public void SimulateEvent()
            {
                OnEventDetected(this, EventArgs.Empty);
            }
        }
    }
}
