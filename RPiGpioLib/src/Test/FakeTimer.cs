using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPi.Gpio.Timers;
using System.Timers;

namespace RPi.Gpio.Test
{
    public sealed class FakeTimer : ITimer
    {
        public event TickEventHandler Tick;

        public FakeTimer() { }

        public void SimulateTick()
        {
            if (Tick != null)
                Tick();
        }

        public void Start()
        {
            Started = true;
        }

        public void Stop()
        {
            Started = false;
        }

        public bool Started { get; private set; }
    }
}
