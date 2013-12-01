using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPi.Gpio.Test
{
    public sealed class FakeOutputPin : AbstractPin, IOutputPin
    {
        private bool state;

        public FakeOutputPin(int pinId)
            : base(pinId)
        {
        }

        public void Clear()
        {
            state = false;
        }

        public bool IsSet()
        {
            return state;
        }

        public void Set()
        {
            state = true;
        }
    }
}
