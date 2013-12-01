using System;

namespace RPi.Gpio
{
    /// <summary>
    /// A pin that is used to fake a real hardware input pin and also simulates events from it.
    /// </summary>
    public sealed class FakeEventInputPin : FakeInputPin, IEventInputPin
    {
        public event EventHandler Pin;

        public FakeEventInputPin(int pinId)
            : base(pinId)
        {
        }

        public void TriggerPinEvent()
        {
            if (Pin != null)
                Pin(this, EventArgs.Empty);
        }
    }
}
