
namespace RPi.Gpio
{
    /// <summary>
    /// A pin that is used to set signal output.
    /// </summary>
    public class OutputPin : AbstractHardwarePin, IOutputPin
    {
        public OutputPin(int pinId, IGpioAccess gpioAccess)
            : base(pinId, gpioAccess)
        {
        }

        public void Set()
        {
            GpioAccess.SetPin(Id);
        }

        public void Clear()
        {
            GpioAccess.ClearPin(Id);
        }

        public bool IsSet()
        {
            return GpioAccess.ReadPin(Id);
        }
    }
}
