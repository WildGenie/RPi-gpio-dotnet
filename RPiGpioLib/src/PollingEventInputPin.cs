using System;
using RPi.Gpio.Timers;
using System.Collections.Generic;
using System.Timers;

namespace RPi.Gpio
{
    public class PollingEventInputPin : EventInputPin, IDisposable
    {
        private ITimer pollTimer;

        public PollingEventInputPin(int pinId, IGpioAccess gpioAccess, ISet<InputPinEventType> eventTypes, ITimer pollTimer)
            : base(pinId, gpioAccess, eventTypes)
        {
            this.pollTimer = pollTimer;
            pollTimer.Tick += new TickEventHandler(Elapsed);
        }

        private void Elapsed()
        {
            bool hasEvent = GpioAccess.ReadPinEvent(Id);
            if (hasEvent)
            {
                GpioAccess.ClearPinEvent(Id);
                OnEventDetected(this, EventArgs.Empty);
            }
        }

        public ITimer PollTimer
        {
            get { return pollTimer; }
        }
    }
}
