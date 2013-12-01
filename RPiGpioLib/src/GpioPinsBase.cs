using System.Collections.Generic;
using RPi.Gpio.Util;

namespace RPi.Gpio
{
    public abstract class GpioPinsBase : GpioPins
    {
        protected readonly IDictionary<int, int> pinNoToIdMap;

        protected GpioPinsBase()
        {
            pinNoToIdMap = new Dictionary<int, int>();
            pinNoToIdMap.Add(7, 4);
            pinNoToIdMap.Add(8, 14);
            pinNoToIdMap.Add(10, 15);
            pinNoToIdMap.Add(11, 17);
            pinNoToIdMap.Add(12, 18);
            pinNoToIdMap.Add(15, 22);
            pinNoToIdMap.Add(16, 23);
            pinNoToIdMap.Add(18, 24);
            pinNoToIdMap.Add(19, 10);
            pinNoToIdMap.Add(21, 9);
            pinNoToIdMap.Add(22, 25);
            pinNoToIdMap.Add(23, 11);
            pinNoToIdMap.Add(24, 8);
            pinNoToIdMap.Add(26, 7);
        }

        public int IdOfPinNumber(int pinNo)
        {
            Preconditions.CheckArgument(HasUsablePinNumber(pinNo), "Pin number does not exist or is not available for use.");
            return pinNoToIdMap[pinNo];
        }

        public bool HasUsablePinNumber(int pinNo)
        {
            return pinNoToIdMap.ContainsKey(pinNo);
        }

        public ISet<int> UsablePinNumbers()
        {
            return new HashSet<int>(pinNoToIdMap.Keys);
        }

        public ISet<int> UsablePinIds()
        {
            return new HashSet<int>(pinNoToIdMap.Values);
        }
    }
}
