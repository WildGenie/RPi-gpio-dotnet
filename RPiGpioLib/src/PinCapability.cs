using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPi.Gpio
{
    public enum PinCapability
    {
        StandardIO,
        I2C,
        UART,
        SPI,
        PWM
    }
}
