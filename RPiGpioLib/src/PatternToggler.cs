using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPi.Gpio.Timers;
using RPi.Gpio.Util;

namespace RPi.Gpio
{
    public class PatternToggler : AbstractToggler
    {
        private ITimer clock;
        private IList<bool> pattern;
        private int state;

        public PatternToggler(IOutputPin pin, ITimer clock, ICollection<bool> pattern)
            : base(pin)
        {
            Preconditions.CheckArgument(pattern.Count > 1);
            this.clock = clock;
            this.pattern = new List<bool>(pattern);
        }

        public override void Start()
        {
            IsStopRequested = false;
            state = 0;
            clock.Tick += new TickEventHandler(Tick);
            if (pattern[0])
                Pin.Set();
            else
                Pin.Clear();
            clock.Start();
            base.Start();
        }

        private void Tick()
        {
            state++;
            if (state == pattern.Count)
            {
                state = 0;
                if (IsStopRequested)
                {
                    Stop();
                    return;
                }
            }

            bool level = pattern[state];
            if (level)
                Pin.Set();
            else
                Pin.Clear();
        }

        public override void Stop()
        {
            base.Stop();
            clock.Stop();
            state = 0;
            Pin.Clear();
            clock.Tick -= new TickEventHandler(Tick);
        }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private int clockMillis;
            private List<bool> pattern = new List<bool>();

            public Builder Clock(int millis)
            {
                clockMillis = millis;
                return this;
            }

            public Builder On()
            {
                pattern.Add(true);
                return this;
            }

            public Builder Off()
            {
                pattern.Add(false);
                return this;
            }

            public PatternToggler Build(IOutputPin pin)
            {
                return new PatternToggler(pin, new TimersTimer(clockMillis), pattern);
            }
        }
    }
}
