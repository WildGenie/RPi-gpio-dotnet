
namespace RPi.Gpio
{
    public class GpioPinsRev1 : GpioPinsBase
    {
        public GpioPinsRev1()
        {
            pinNoToIdMap.Add(3, 0);
            pinNoToIdMap.Add(5, 1);
            pinNoToIdMap.Add(13, 21);
        }
    }
}
