using System;

namespace RPi.Gpio
{
    public interface IInputPin
    {
        bool ReadLevel();
    }
}
