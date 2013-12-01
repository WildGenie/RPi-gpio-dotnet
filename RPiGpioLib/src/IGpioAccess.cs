using System;

namespace RPi.Gpio
{
    public interface IGpioAccess
    {
        void ConfigAsAlternateFunction(int pinId, int function);

        void ConfigAsInput(int pinId);

        void ConfigAsOutput(int pinId);

        void ConfigRisingEdgeDetection(int pinId, bool enable);

        void ConfigFallingEdgeDetection(int pinId, bool enable);

        void ConfigAsyncRisingEdgeDetection(int pinId, bool enable);

        void ConfigAsyncFallingEdgeDetection(int pinId, bool enable);

        void ConfigLevelHighDetection(int pinId, bool enable);

        void ConfigLevelLowDetection(int pinId, bool enable);

        bool ReadPin(int pinId);

        bool ReadPinEvent(int pinId);

        void ClearPinEvent(int pinId);

        void SetPin(int pinId);

        void ClearPin(int pinId);
    }
}
