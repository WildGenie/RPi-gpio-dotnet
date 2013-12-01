using System.Collections.Generic;

namespace RPi.Gpio
{
    public interface GpioPins
    {
        int IdOfPinNumber(int pinNo);

        bool HasUsablePinNumber(int pinNo);

        ISet<int> UsablePinNumbers();

        ISet<int> UsablePinIds();
    }
}
