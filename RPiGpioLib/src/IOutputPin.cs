using System;

namespace RPi.Gpio
{
    public interface IOutputPin
    {
        void Clear();

        bool IsSet();

        void Set();
    }
}
