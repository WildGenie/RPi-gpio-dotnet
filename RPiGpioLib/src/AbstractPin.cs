using System;

namespace RPi.Gpio
{
    public abstract class AbstractPin
    {
        private int pinId;

        protected AbstractPin(int pinId)
        {
            this.pinId = pinId;
        }

        public int Id { get { return pinId; } }
    }
}
