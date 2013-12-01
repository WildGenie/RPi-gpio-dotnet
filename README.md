rpi-gpio
========
Lets you access the Raspberry Pi's hardware pins in an easy and intuitive way. It strives for full testability of the code base and therefore making the library fit for today's quality standards and simple for future extensions. That shortens the development cycle and makes you more productive instead of struggling with low-level issues. Every functionality provides a fake implementation for testing purposes. Thus bugs are detected in the early stage of your application and remote debugging will be something from the past. Hardware development has not ever been more fun!


Features
--------
* Configure pins
    * as input
    * as output
    * for all existing alternate functions
    * for rising and falling edge detection (synchronously and asynchronously)
    * for 'low' and 'high' level detection
    * with pull-up/down resistors
* Setting state of output pins (through direct memory access)
    * manually
    * toggling - on-off time, clock with on-off pattern
* Reading state of input pins
    * manually
    * automatically by polling
* Getting notified about state changes of input pins
    * manually
    * automatically by polling


Future Extensions
-----------------
* Getting notified about state changes of input pins
    * through an interrupt
    * by polling
* Pulse-width modulation output
* SPI interface
* UART interface
* I2C interface
* PCM output
