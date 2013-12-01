using System;
using System.Threading;

namespace RPi.Gpio.Test.Device
{
    public class Pin4InputTest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Pin 4 (= HW pin no. 7) input test. Reads logical level every 100ms 5 seconds long.");
            GpioOverMemory.MemoryMap memMap = GpioOverMemory.MemoryMapCreator.Create();
            GpioOverMemory gpio = new GpioOverMemory(memMap.Address);
            gpio.ConfigAsInput(4);

            for (int i = 0; i < 50; i++)
            {
                bool level = gpio.ReadPin(4);
                Console.WriteLine("Logical level of pin 4 = " + level.ToString());
                Thread.Sleep(100);
            }

            Console.WriteLine("Releasing unmanaged resources of test.");
            memMap.Dispose();
        }
    }
}
