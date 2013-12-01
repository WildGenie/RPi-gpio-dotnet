using System;
using System.Threading;

namespace RPi.Gpio.Test.Device
{
    public class Pin4OutputTest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Pin 4 (= HW pin no. 7) output test. LED blinks for 10 seconds.");
            GpioOverMemory.MemoryMap memMap = GpioOverMemory.MemoryMapCreator.Create();
            GpioOverMemory gpio = new GpioOverMemory(memMap.Address);
            gpio.ConfigAsInput(4); // Always use ConfigAsInput() before although you want it to be an output!
            gpio.ConfigAsOutput(4);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("On.");
                gpio.SetPin(4);
                Thread.Sleep(500);
                Console.WriteLine("Off.");
                gpio.ClearPin(4);
                Thread.Sleep(500);
            }

            Console.WriteLine("Releasing unmanaged resources of test.");
            memMap.Dispose();
        }
    }
}
