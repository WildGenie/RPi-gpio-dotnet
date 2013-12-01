using System.Timers;

namespace RPi.Gpio.Timers
{
    public sealed class TimersTimer : ITimer
    {
        public event TickEventHandler Tick;
        private Timer timer;

        public TimersTimer(double interval)
        {
            this.timer = new Timer(interval);
            this.timer.Elapsed += new ElapsedEventHandler(Elapsed);
        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Tick != null)
                Tick();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
