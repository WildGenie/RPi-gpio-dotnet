using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPi.Gpio.Timers;
using System.Timers;
using System.Threading.Tasks;

namespace RPi.Gpio
{
    public class TimeToggler : AbstractToggler
    {
        private ITimer onTimer;
        private ITimer offTimer;

        public TimeToggler(IOutputPin pin, ITimer onTimer, ITimer offTimer)
            : base(pin)
        {
            this.onTimer = onTimer;
            this.offTimer = offTimer;
        }

        public override void Start()
        {
            IsStopRequested = false;
            onTimer.Tick += new TickEventHandler(OnTick);
            offTimer.Tick += new TickEventHandler(OffTick);
            onTimer.Start();
            offTimer.Stop();
            base.Start();
            Pin.Set();
        }

        private void OnTick()
        {
            if (IsStopRequested)
            {
                Stop();
                return;
            }

            onTimer.Stop();
            offTimer.Start();
            Pin.Clear();
        }

        private void OffTick()
        {
            if (IsStopRequested)
            {
                Stop();
                return;
            }

            onTimer.Start();
            offTimer.Stop();
            Pin.Set();
        }

        public override void Stop()
        {
            base.Stop();
            onTimer.Stop();
            offTimer.Stop();
            Pin.Clear();
            onTimer.Tick -= new TickEventHandler(OnTick);
            offTimer.Tick -= new TickEventHandler(OffTick);
        }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private int onMillis;
            private int offMillis;
            
            public Builder OnTime(int millis)
            {
                onMillis = millis;
                return this;
            }

            public Builder OffTime(int millis)
            {
                offMillis = millis;
                return this;
            }

            public TimeToggler Build(IOutputPin pin)
            {
                return new TimeToggler(pin, new TimersTimer(onMillis), new TimersTimer(offMillis));
            }
        }
    }
}
