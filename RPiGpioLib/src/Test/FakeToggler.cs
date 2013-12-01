using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPi.Gpio.Test
{
    public sealed class FakeToggler : IStartStopable
    {
        public void Start()
        {
            Started = true;
        }

        public void Stop()
        {
            Started = false;
        }

        public void RequestStop()
        {
            Started = false;
        }

        public bool Started
        {
            get;
            private set;
        }
    }
}
