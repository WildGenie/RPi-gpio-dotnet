using System.Timers;

namespace RPi.Gpio.Timers
{
    public interface ITimer : IStartStopable
    {
        event TickEventHandler Tick;
    }

    public delegate void TickEventHandler();
}
