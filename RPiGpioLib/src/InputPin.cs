
namespace RPi.Gpio
{
    /// <summary>
    /// A pin that is used as a signal input.
    /// </summary>
    public class InputPin : AbstractHardwarePin, IInputPin
    {
        public InputPin(int pinId, IGpioAccess gpioAccess)
            : base(pinId, gpioAccess)
        {
        }

        public bool ReadLevel()
        {
            return GpioAccess.ReadPin(Id);
        }
    }
}
