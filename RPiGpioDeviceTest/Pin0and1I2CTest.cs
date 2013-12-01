using System;
using System.Threading;

namespace RPi.Gpio.Test.Device
{
    public class Pin0and1I2CTest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Pin 4 (= HW pin no. 7) output test. LED blinks for 10 seconds.");
            GpioOverMemory.MemoryMap memMap = GpioOverMemory.MemoryMapCreator.Create();
            GpioOverMemory gpio = new GpioOverMemory(memMap.Address);
            gpio.ConfigAsInput(0); // Always use ConfigAsInput() before although you want it to be an alternate function!
            gpio.ConfigAsAlternateFunction(0, 0);
            gpio.ConfigAsInput(1); // Always use ConfigAsInput() before although you want it to be an alternate function!
            gpio.ConfigAsAlternateFunction(1, 0);


            Console.WriteLine("Releasing unmanaged resources of test.");
            memMap.Dispose();
        }
    }
}
