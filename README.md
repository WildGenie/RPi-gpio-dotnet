Raspberry Pi GPIO Pure .NET Library
==============================
Lets you access the Raspberry Pi's hardware pins in an easy and intuitive way taking advantage of pure object-oriented .NET languages such as C#, VB.NET and others without any dependencies to native libraries written in C or Assembly.

This library aims to fully implement the general-purpose input/output, short GPIO, interface hardware capabilities of the Raspberry Pi in a pure object-oriented approach. Additionally it provides commonly-required functionality around them. Nonetheless the footprint is kept as low as possible.

It strives for full testability and therefore making the library fit for today's quality standards and simple for future extensions. That shortens the development cycle and makes you more productive instead of struggling with low-level issues. You can confidently focus on your core-business.  
Every hardware-related interface implementation also offers a fake implementation for unit testing and integration testing purposes. Thus bugs are detected in the early stage of your application and remote debugging will be something from the past.  
Hardware development has not ever been more fun!


Features
--------
* Configure pins
    * as input
    * as output
    * for all existing alternate functions
    * for rising and falling edge detection (synchronously and asynchronously)
    * for 'low' and 'high' level detection
    * with pull-up/down resistors
* Switch input/output configuration of pins at runtime
* Setting state of output pins
    * manually
    * toggling - on-off time, clock with on-off pattern
* Reading state of input pins
    * manually
    * automatically by polling
* Getting notified about state changes of input pins
    * manually
    * automatically by polling

__Note:__ Access to hardware is done through fast direct memory access.


Future Extensions
-----------------
* Getting notified about state changes of input pins
    * through an interrupt (access over file system)
    * by polling
* Pulse-width modulation output
* SPI interface
* UART interface
* I2C interface
* PCM output


Prerequisites
-------------
* .NET 4 framework installed on your development computer
* Microsoft Test (MSTest) - is intended to be migrated to NUnit soon to support Mac/Linux developers
* Raspberry Pi (only for hardware tests)
    * Installed Mono for .NET 4 framework - depends on running OS
    * LED with resistor - for testing output pins
    * switch with 10kOhm resistor (pull-up or -down depending on if closed is 'low' or 'high' level) - for testing input pins


Usage
-----
__TODO__
