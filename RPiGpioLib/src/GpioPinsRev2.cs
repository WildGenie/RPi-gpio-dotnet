
namespace RPi.Gpio
{
    public class GpioPinsRev2 : GpioPinsBase
    {
        public GpioPinsRev2()
        {
            pinNoToIdMap.Add(3, 2);
            pinNoToIdMap.Add(5, 3);
            pinNoToIdMap.Add(13, 27);
        }
    }
}
