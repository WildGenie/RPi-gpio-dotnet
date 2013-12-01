using System;
using System.Collections.Generic;

namespace RPi.Gpio
{
    public abstract class EventInputPin : InputPin, IEventInputPin, IDisposable
    {
        private ISet<InputPinEventType> types;
        public event EventHandler Pin;

        public EventInputPin(int pinId, IGpioAccess gpioAccess, ISet<InputPinEventType> types)
            : base(pinId, gpioAccess)
        {
            this.types = new HashSet<InputPinEventType>(types);
        }

        protected void OnEventDetected(object sender, EventArgs args)
        {
            if (Pin != null)
                Pin(sender, args);
        }

        public void Dispose()
        {
            foreach (InputPinEventType type in EventTypes)
            {
                if (type == InputPinEventType.RisingEdge)
                    GpioAccess.ConfigRisingEdgeDetection(Id, false);
                else if (type == InputPinEventType.FallingEdge)
                    GpioAccess.ConfigFallingEdgeDetection(Id, false);
                else if (type == InputPinEventType.LevelHigh)
                    GpioAccess.ConfigLevelHighDetection(Id, false);
                else if (type == InputPinEventType.LevelLow)
                    GpioAccess.ConfigLevelLowDetection(Id, false);
            }
        }

        public ISet<InputPinEventType> EventTypes
        {
            get { return types; }
        }
    }
}
