using System;
using System.Collections.Generic;

namespace RPi.Gpio.Util
{
    public static class Bits
    {
        public static bool ReadBit(int value, int bitNo)
        {
            int bitmask = 1 << bitNo;
            return (value & bitmask) == bitmask;
        }

        public static int ClearBitsFromMask(int value, int bitmask)
        {
            return value & (~bitmask);
        }

        public static int ClearBits(int value, ISet<int> bitPositions)
        {
            Preconditions.CheckArgument(bitPositions.Count > 0, "Minimum one bit must be provided.");
            int bitmask = CompoundMask(bitPositions);
            return ClearBitsFromMask(value, bitmask);
        }

        public static int CompoundMask(ISet<int> bitPositions)
        {
            int bitmask = 0;
            foreach (int bitPos in bitPositions)
                bitmask |= 1 << bitPos;
            return bitmask;
        }

        public static int SetBitsFromMask(int value, int bitmask)
        {
            return value | bitmask;
        }

        public static int SetBits(int value, ISet<int> bitPositions)
        {
            Preconditions.CheckArgument(bitPositions.Count > 0, "Minimum one bit must be provided.");
            int bitmask = CompoundMask(bitPositions);
            return SetBitsFromMask(value, bitmask);
        }

        public static int ClearBit(int value, int bitNo)
        {
            return ClearBitsFromMask(value, 1 << bitNo); // Clear bit at position
        }

        public static int SetBit(int value, int bitNo)
        {
            return SetBitsFromMask(value, 1 << bitNo); // Set bit at position
        }

        public static int SetClearBit(int value, int bitNo, bool enable)
        {
            int newValue;
            if (enable)
                newValue = SetBitsFromMask(value, 1 << bitNo); // Set bit at position
            else
                newValue = ClearBitsFromMask(value, 1 << bitNo); // Clear bit at position
            return newValue;
        }

        public static string ToBitString(int number)
        {
            if (number == 0)
                return "0000-0000-0000-0000-0000-0000-0000-0000";

            char[] bits = new char[39];
            for (int i = 0; i < 39; i++)
            {
                if (i == 4 || i > 6 && ((i - 4) % 5) == 0)
                    bits[i] = '-';
                else
                    bits[i] = '0';
            }

            int j = 0;
            while (number != 0)
            {
                if (j > 38)
                    break;
                if (j == 4 || j > 6 && ((j - 4) % 5) == 0)
                    j++;
                bits[j++] = (number & 1) == 1 ? '1' : '0';
                number >>= 1;
            }

            Array.Reverse(bits, 0, 39);
            return new string(bits);
        }
    }
}
