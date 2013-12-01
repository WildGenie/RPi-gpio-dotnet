using System;

namespace RPi.Gpio
{
    public abstract class AbstractHardwarePin : AbstractPin
    {
        private IGpioAccess gpioAccess;

        protected AbstractHardwarePin(int pinId, IGpioAccess gpioAccess):base(pinId)
        {
            this.gpioAccess = gpioAccess;
        }

        protected IGpioAccess GpioAccess { get { return gpioAccess; } }
    }
}
