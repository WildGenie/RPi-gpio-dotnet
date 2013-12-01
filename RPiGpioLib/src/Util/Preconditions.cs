using System;

namespace RPi.Gpio.Util
{
    public static class Preconditions
    {
        public static object CheckNotNull(object reference)
        {
            if (reference == null)
                throw new ArgumentNullException();
            return reference;
        }

        public static object CheckNotNull(object reference, string message)
        {
            if (reference == null)
                throw new ArgumentNullException(message);
            return reference;
        }

        public static void CheckArgument(bool condition)
        {
            if (!condition)
                throw new ArgumentException();
        }

        public static void CheckArgument(bool condition, string message)
        {
            if (!condition)
                throw new ArgumentException(message);
        }

        public static void CheckState(bool condition)
        {
            if (!condition)
                throw new InvalidOperationException();
        }

        public static void CheckState(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }
    }
}
