
namespace RPi.Gpio
{
    /// <summary>
    /// A pin that is used to fake a real hardware input pin.
    /// </summary>
    public class FakeInputPin : AbstractPin, IInputPin
    {
        public FakeInputPin(int pinId)
            : base(pinId)
        {
        }

        public bool ReadLevel()
        {
            return Level;
        }

        public bool Level
        {
            set;
            get;
        }
    }
}
