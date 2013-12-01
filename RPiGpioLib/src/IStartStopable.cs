using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPi.Gpio
{
    public interface IStartStopable
    {
        void Start();

        void Stop();
    }
}
