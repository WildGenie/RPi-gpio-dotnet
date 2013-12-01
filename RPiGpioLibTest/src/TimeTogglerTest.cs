using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPi.Gpio.Timers;
using RPi.Gpio;

namespace RPi.Gpio.Test
{
    [TestClass]
    public class TimeTogglerTest
    {
        [TestMethod]
        public void Build()
        {
            IOutputPin pin = null;
            TimeToggler toggler = TimeToggler.NewBuilder().OnTime(500).OffTime(300).Build(pin);
            Assert.IsNotNull(toggler);
        }

        [TestMethod]
        public void Toggle()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer onTimer = new FakeTimer();
            FakeTimer offTimer = new FakeTimer();
            TimeToggler toggler = new TimeToggler(pin, onTimer, offTimer);

            Assert.IsFalse(pin.IsSet());
            Assert.IsFalse(onTimer.Started);
            Assert.IsFalse(offTimer.Started);
            toggler.Start();
            Assert.IsTrue(pin.IsSet());
            Assert.IsTrue(onTimer.Started);
            Assert.IsFalse(offTimer.Started);

            onTimer.SimulateTick();
            Assert.IsFalse(pin.IsSet());
            Assert.IsFalse(onTimer.Started);
            Assert.IsTrue(offTimer.Started);

            offTimer.SimulateTick();
            Assert.IsTrue(pin.IsSet());
            Assert.IsTrue(onTimer.Started);
            Assert.IsFalse(offTimer.Started);

            toggler.Stop();
            Assert.IsFalse(pin.IsSet());
            Assert.IsFalse(onTimer.Started);
            Assert.IsFalse(offTimer.Started);
        }

        [TestMethod]
        public void RequestStopDuringPinHigh()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer onTimer = new FakeTimer();
            FakeTimer offTimer = new FakeTimer();
            TimeToggler toggler = new TimeToggler(pin, onTimer, offTimer);

            toggler.Start();
            toggler.RequestStop();
            onTimer.SimulateTick();
            Assert.IsFalse(pin.IsSet());
            Assert.IsFalse(onTimer.Started);
            Assert.IsFalse(offTimer.Started);
        }

        [TestMethod]
        public void RequestStopDuringPinLow()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer onTimer = new FakeTimer();
            FakeTimer offTimer = new FakeTimer();
            TimeToggler toggler = new TimeToggler(pin, onTimer, offTimer);

            toggler.Start();
            onTimer.SimulateTick();
            toggler.RequestStop();
            offTimer.SimulateTick();
            Assert.IsFalse(pin.IsSet());
            Assert.IsFalse(onTimer.Started);
            Assert.IsFalse(offTimer.Started);
        }

        [TestMethod]
        public void NoTickWithoutStart()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer onTimer = new FakeTimer();
            FakeTimer offTimer = new FakeTimer();
            TimeToggler toggler = new TimeToggler(pin, onTimer, offTimer);

            onTimer.SimulateTick();
            Assert.IsFalse(pin.IsSet());
            offTimer.SimulateTick();
            Assert.IsFalse(pin.IsSet());
        }

        [TestMethod]
        public void StopWithoutStart()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer onTimer = new FakeTimer();
            FakeTimer offTimer = new FakeTimer();
            TimeToggler toggler = new TimeToggler(pin, onTimer, offTimer);

            toggler.Stop();
        }
    }
}
