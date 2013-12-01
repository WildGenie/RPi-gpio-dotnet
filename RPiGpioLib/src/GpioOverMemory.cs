using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Mono.Unix.Native;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RPi.Gpio.Util;

namespace RPi.Gpio
{
    public sealed class GpioOverMemory : IGpioAccess
    {
        /// <summary>
        /// Pin Output Set
        /// </summary>
        private const int GPSET0 = 0x1C;
        /// <summary>
        /// Pin Output Clear
        /// </summary>
        private const int GPCLR0 = 0x28;
        /// <summary>
        /// Pin Level
        /// </summary>
        private const int GPLEV0 = 0x34;
        /// <summary>
        /// Pin Event Detect Status
        /// </summary>
        private const int GPEDS0 = 0x40;
        /// <summary>
        /// Pin Rising Edge Detect Enable
        /// </summary>
        private const int GPREN0 = 0x4C;
        /// <summary>
        /// Pin Falling Edge Detect Enable
        /// </summary>
        private const int GPFEN0 = 0x58;
        /// <summary>
        /// Pin High Detect Enable
        /// </summary>
        private const int GPHEN0 = 0x64;
        /// <summary>
        /// Pin Low Detect Enable
        /// </summary>
        private const int GPLEN0 = 0x70;
        /// <summary>
        /// Pin Async Rising Edge Detect
        /// </summary>
        private const int GPAREN0 = 0x7C;
        /// <summary>
        /// Pin Async Falling Edge Detect
        /// </summary>
        private const int GPAFEN0 = 0x88;
        /// <summary>
        /// Pin Pull-up/down Enable
        /// </summary>
        private const int GPPUD = 0x94;
        /// <summary>
        /// Pin Pull-up/down Enable Clock
        /// </summary>
        private const int GPPUDCLK0 = 0x98;

        private IntPtr gpioMemMapAddr;

        public GpioOverMemory(IntPtr gpioMemMapAddress)
        {
            Preconditions.CheckArgument(gpioMemMapAddress != IntPtr.Zero, "The memory mapping pointer must not be zero.");
            this.gpioMemMapAddr = gpioMemMapAddress;
        }

        public void ConfigAsInput(int pinId)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " as input");
            int offset = pinId / 10;
            int currValue = Marshal.ReadInt32(gpioMemMapAddr, offset);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(currValue) + "\" at offset address " + offset);
            int bitmask = 7 << ((pinId % 10) * 3);
            int newValue = Bits.ClearBitsFromMask(currValue, bitmask);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address " + offset);
            Marshal.WriteInt32(gpioMemMapAddr, offset, newValue);
        }

        public void ConfigAsOutput(int pinId)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " as output");
            int offset = pinId / 10;
            int currValue = Marshal.ReadInt32(gpioMemMapAddr, offset);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(currValue) + "\" at offset address " + offset);
            int bitmask = 1 << ((pinId % 10) * 3);
            int newValue = Bits.SetBitsFromMask(currValue, bitmask);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address " + offset);
            Marshal.WriteInt32(gpioMemMapAddr, offset, newValue);
        }

        public void ConfigAsAlternateFunction(int pinId, int function)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for alternate function");
            int offset = pinId / 10;
            int currValue = Marshal.ReadInt32(gpioMemMapAddr, offset);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(currValue) + "\" at offset address " + offset);
            int bitmask = (function <= 3 ? function + 4 : function == 4 ? 3 : 2) << ((pinId % 10) * 3);
            int newValue = Bits.SetBitsFromMask(currValue, bitmask);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address " + offset);
            Marshal.WriteInt32(gpioMemMapAddr, offset, newValue);
        }

        public void ConfigRisingEdgeDetection(int pinId, bool enable)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for rising edge event detection");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPREN0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 19");
            int newValue = Bits.SetClearBit(value, pinId, enable);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 19");
            Marshal.WriteInt32(gpioMemMapAddr, GPREN0, newValue);
        }

        public void ConfigAsyncRisingEdgeDetection(int pinId, bool enable)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for asynchronous rising edge event detection");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPAREN0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 19");
            int newValue = Bits.SetClearBit(value, pinId, enable);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 19");
            Marshal.WriteInt32(gpioMemMapAddr, GPAREN0, newValue);
        }

        public void ConfigFallingEdgeDetection(int pinId, bool enable)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for falling edge event detection");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPFEN0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 22");
            int newValue = Bits.SetClearBit(value, pinId, enable);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 22");
            Marshal.WriteInt32(gpioMemMapAddr, GPFEN0, newValue);
        }

        public void ConfigAsyncFallingEdgeDetection(int pinId, bool enable)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for asynchronous falling edge event detection");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPAFEN0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 22");
            int newValue = Bits.SetClearBit(value, pinId, enable);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 22");
            Marshal.WriteInt32(gpioMemMapAddr, GPAFEN0, newValue);
        }

        public void ConfigLevelHighDetection(int pinId, bool enable)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for level \"high\" event detection");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPHEN0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 25");
            int newValue = Bits.SetClearBit(value, pinId, enable);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 25");
            Marshal.WriteInt32(gpioMemMapAddr, GPHEN0, newValue);
        }

        public void ConfigLevelLowDetection(int pinId, bool enable)
        {
            Debug.WriteLine("Config pin " + pinId.ToString() + " for level \"low\" event detection");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPLEN0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 28");
            int newValue = Bits.SetClearBit(value, pinId, enable);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 28");
            Marshal.WriteInt32(gpioMemMapAddr, GPLEN0, newValue);
        }

        public void ConfigPullUpDown(ISet<int> pinIds, PullUpDown control)
        {
            /*
             * Sequence of execution that must be followed:
             * 
             * 1. Write to GPPUD to set the required control signal (i.e. Pull-up or Pull-Down or
             *    neither to remove the current Pull-up/down) 
             *    
             * 2. Wait 150 cycles – this provides the required set-up time for the control signal 
             * 
             * 3. Write to GPPUDCLK0/1 to clock the control signal into the GPIO pads you wish
             *    to modify – NOTE only the pads which receive a clock will be modified,
             *    all others will retain their previous state. 
             *    
             * 4. Wait 150 cycles – this provides the required hold time for the control signal 
             * 
             * 5. Write to GPPUD to remove the control signal 
             * 
             * 6. Write to GPPUDCLK0/1 to remove the clock
             */
            Preconditions.CheckArgument(pinIds.Count > 0, "Minimum one pin must be set.");
            Debug.WriteLine("Config pins " + pinIds.ToString() + " for " + control + " control");

            int value = Marshal.ReadInt32(gpioMemMapAddr, GPPUD);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 37");

            int newValue;
            if (control == PullUpDown.Up)
                newValue = Bits.SetBitsFromMask(value, 2); // 10b --> 2d
            else if (control == PullUpDown.Down)
                newValue = Bits.SetBitsFromMask(value, 1); // 01b --> 1d
            else
                throw new InvalidOperationException();
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 37");
            Marshal.WriteInt32(gpioMemMapAddr, GPPUD, newValue);

            ShortSleep();

            value = Marshal.ReadInt32(gpioMemMapAddr, GPPUDCLK0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 38");
            newValue = Bits.SetBits(value, pinIds);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 38");
            Marshal.WriteInt32(gpioMemMapAddr, GPPUDCLK0, newValue);

            ShortSleep();

            value = Marshal.ReadInt32(gpioMemMapAddr, GPPUD);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 37");
            newValue = Bits.ClearBitsFromMask(value, 3); // 11b = 3d
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 37");
            Marshal.WriteInt32(gpioMemMapAddr, GPPUD, newValue);

            value = Marshal.ReadInt32(gpioMemMapAddr, GPPUDCLK0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 38");
            newValue = Bits.ClearBits(value, pinIds);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 38");
            Marshal.WriteInt32(gpioMemMapAddr, GPPUDCLK0, newValue);
        }

        /// <summary>
        /// Should wait 150 CPU cycles according to datasheet.
        /// The clock frequency is unknown so time is also unknown. We assume 1 MHz which is very low.
        /// See also http://docs.huihoo.com/doxygen/linux/kernel/3.7/pinctrl-bcm2835_8c_source.html
        /// </summary>
        private void ShortSleep()
        {
            Timespec req = new Timespec() { tv_nsec = 150 };
            Timespec rem = new Timespec();
            Syscall.nanosleep(ref req, ref rem);
        }

        public void SetPin(int pinId)
        {
            Debug.WriteLine("Set pin " + pinId.ToString() + " to level \"high\"");
            int value = 1 << pinId; // Touching other bits does not affect their value. Bit must be explicitly cleared by setting bit in clear register.
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(value) + "\" at offset address 7");
            Marshal.WriteInt32(gpioMemMapAddr, GPSET0, value);
        }

        public void ClearPin(int pinId)
        {
            Debug.WriteLine("Set pin " + pinId.ToString() + " to level \"low\"");
            int value = 1 << pinId; // Touching other bits does not affect their value. Bit must be explicitly set by setting bit in enable register.
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(value) + "\" at offset address 10");
            Marshal.WriteInt32(gpioMemMapAddr, GPCLR0, value);
        }

        public bool ReadPin(int pinId)
        {
            Debug.WriteLine("Read level of pin " + pinId.ToString());
            int currValue = Marshal.ReadInt32(gpioMemMapAddr, GPLEV0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(currValue) + "\" at offset address 13");
            return Bits.ReadBit(currValue, pinId);
        }

        public bool ReadPinEvent(int pinId)
        {
            Debug.WriteLine("Read pin " + pinId.ToString() + " event");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPEDS0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 16");
            return Bits.ReadBit(value, pinId);
        }

        public void ClearPinEvent(int pinId)
        {
            Debug.WriteLine("Clear pin " + pinId.ToString() + " event");
            int value = Marshal.ReadInt32(gpioMemMapAddr, GPEDS0);
            Debug.WriteLine("Value of register is \"" + Bits.ToBitString(value) + "\" at offset address 16");
            int newValue = Bits.SetClearBit(value, pinId, false);
            Debug.WriteLine("Setting register to \"" + Bits.ToBitString(newValue) + "\" at offset address 16");
            Marshal.WriteInt32(gpioMemMapAddr, GPEDS0, newValue);
        }

        public enum PullUpDown
        {
            Up,
            Down
        }

        public static class MemoryMapCreator
        {
            private const long BCM2708_PERIPHERALS_BASE = 0x20000000;
            private const long GPIO_BASE = BCM2708_PERIPHERALS_BASE + 0x200000;
            /// <summary>
            /// Size of the memory map. Must be multiples of the page size.
            /// </summary>
            private const ulong BLOCK_SIZE = 1024 * 4;

            /// <summary>
            /// Creates the memory mapping. Calling this method twice does not have any effect.
            /// </summary>
            public static MemoryMap Create()
            {
                int fileDescriptor = Syscall.open("/dev/mem", OpenFlags.O_RDWR | OpenFlags.O_SYNC);
                if (fileDescriptor < 0)
                    throw new MemoryMapException("Error on calling \"open()\". Could not open file \"/dev/mem\".");

                IntPtr addr = Syscall.mmap(IntPtr.Zero, BLOCK_SIZE, MmapProts.PROT_READ | MmapProts.PROT_WRITE, MmapFlags.MAP_SHARED, fileDescriptor, GPIO_BASE);
                if (addr == IntPtr.Zero)
                {
                    Syscall.close(fileDescriptor);
                    throw new MemoryMapException("Error on calling \"mmap()\". Could not create GPIO memory mapping.");
                }

                Syscall.close(fileDescriptor);
                return new MemoryMap(addr, BLOCK_SIZE);
            }
        }

        /// <summary>
        /// Manages the lifecycle of the GPIO memory map. Creates, holds and releases the resources associated with the memory mapping.
        /// <para/>
        /// Always call <see cref="Dispose()"/> when finished using the memory mapping. Usually it is done at the end of programm execution.
        /// </summary>
        public sealed class MemoryMap : IDisposable
        {
            private IntPtr address;
            private ulong size;

            internal MemoryMap(IntPtr address, ulong size)
            {
                this.address = address;
                this.size = size;
            }

            public void Dispose()
            {
                Debug.WriteLine("Unmapping GPIO memory mapping");
                int res = Syscall.munmap(address, size);
                if (res == -1)
                    Debug.Fail("Cannot unmap GPIO memory mapping");
            }

            public IntPtr Address
            {
                get
                {
                    return address;
                }
            }
        }

        [Serializable]
        public class MemoryMapException : Exception
        {
            public MemoryMapException() { }

            public MemoryMapException(string message)
                : base(message)
            {
            }

            public MemoryMapException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected MemoryMapException(SerializationInfo info, StreamingContext context)
            {
            }
        }
    }
}
