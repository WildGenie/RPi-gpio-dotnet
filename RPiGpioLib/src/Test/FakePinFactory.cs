using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPi.Gpio.Test
{
    public static class FakePinFactory
    {
        public static FakeInputPin InputPin(int pinId)
        {
            return new FakeInputPin(pinId);
        }

        public static FakeOutputPin OutputPin(int pinId)
        {
            return new FakeOutputPin(pinId);
        }

        public static FakeEventInputPin EventInputPin(int pinId)
        {
            return new FakeEventInputPin(pinId);
        }
    }
}
