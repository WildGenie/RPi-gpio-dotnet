using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPi.Gpio
{
    public abstract class AbstractToggler : IStartStopable, IRequestStop
    {
        private IOutputPin pin;

        protected AbstractToggler(IOutputPin pin)
        {
            this.pin = pin;
        }

        public IOutputPin Pin
        {
            get { return pin; }
        }

        public bool IsRunning
        {
            get;
            private set;
        }

        protected bool IsStopRequested
        {
            get;
            set;
        }

        public virtual void Start()
        {
            IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
        }

        public void RequestStop()
        {
            IsStopRequested = true;
        }
    }
}
