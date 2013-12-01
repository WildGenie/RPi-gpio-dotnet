using System.Collections.Generic;
using RPi.Gpio.Timers;

namespace RPi.Gpio
{
    public sealed class GpioConfig
    {
        private IGpioAccess gpio;

        public GpioConfig(IGpioAccess gpio)
        {
            this.gpio = gpio;
        }

        public InputPin InputPin(int pinId)
        {
            gpio.ConfigAsInput(pinId);
            return new InputPin(pinId, gpio);
        }

        public OutputPin OutputPin(int pinId)
        {
            gpio.ConfigAsOutput(pinId);
            return new OutputPin(pinId, gpio);
        }

        public PollingEventInputPin PollingEventInputPin(int pinId, ISet<InputPinEventType> types, ITimer pollTimer)
        {
            ConfigEvents(pinId, types);
            return new PollingEventInputPin(pinId, gpio, types, pollTimer);
        }

        public PollingEventInputPin PollingEventInputPin(int pinId, ITimer pollTimer, params InputPinEventType[] types)
        {
            HashSet<InputPinEventType> eTypes = new HashSet<InputPinEventType>();
            foreach (InputPinEventType t in types)
                eTypes.Add(t);
            ConfigEvents(pinId, eTypes);
            return new PollingEventInputPin(pinId, gpio, eTypes, pollTimer);
        }

        private void ConfigEvents(int pinId, ISet<InputPinEventType> types)
        {
            foreach (InputPinEventType type in types)
            {
                if (type == InputPinEventType.RisingEdge)
                    gpio.ConfigRisingEdgeDetection(pinId, true);
                else if (type == InputPinEventType.FallingEdge)
                    gpio.ConfigFallingEdgeDetection(pinId, true);
                else if (type == InputPinEventType.LevelHigh)
                    gpio.ConfigLevelHighDetection(pinId, true);
                else if (type == InputPinEventType.LevelLow)
                    gpio.ConfigLevelLowDetection(pinId, true);
            }
        }
    }
}
