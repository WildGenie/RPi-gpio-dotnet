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
    public class PatternTogglerTest
    {

        [TestMethod]
        public void Build()
        {
            IOutputPin pin = null;
            PatternToggler toggler = PatternToggler.NewBuilder().Clock(200).On().On().On().Off().On().Build(pin);
            Assert.IsNotNull(toggler);
        }

        [TestMethod]
        public void PatternTrueFalse()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer clock = new FakeTimer();
            bool[] pattern = { true, false };
            PatternToggler toggler = new PatternToggler(pin, clock, pattern.ToList());
            toggler.Start();
            Assert.IsTrue(pin.IsSet());
            clock.SimulateTick();
            Assert.IsFalse(pin.IsSet());
            clock.SimulateTick();
            Assert.IsTrue(pin.IsSet());
        }

        [TestMethod]
        public void PatternFalseTrue()
        {
            FakeOutputPin pin = new FakeOutputPin(1);
            FakeTimer clock = new FakeTimer();
            bool[] pattern = { false, true };
            PatternToggler toggler = new PatternToggler(pin, clock, pattern.ToList());
            toggler.Start();
            Assert.IsFalse(pin.IsSet());
            clock.SimulateTick();
            Assert.IsTrue(pin.IsSet());
            clock.SimulateTick();
            Assert.IsFalse(pin.IsSet());
        }
    }
}
