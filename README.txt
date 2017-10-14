The goal of this project is the get inputs from arrival to the OS as fast as possible. This is the reason I've created a separate solution for this function instead of just a library I can import when I need it. As I progress on my current ideas, I may decide to deprecate this and just use it as a library in each application, maybe with events

How it works currently (in early stages):

Inner workings...
1. Reads in an "Input Map" file, which maps an input name and an index to a specific scancode value which is defined in Windows.
2. These inputs are stored in the InputController which keeps track of the current state of each input value.
3. The scancode values are sent to the OS via the SendInput() WINAPI method imported from User32.dll and 

Hooks...
1. Creates a Memory Mapped File. Currently this name must be copied exactly in the code that will be utilizing this controller, so it must be predetermined or pulled from a shared resource.
2. The value read from this memory mapped file is is just a value where each bit represents the state of an input. The bit is mapped to a specific input via a "position" value in the shared input map config file. Only differences between values are read so there is no need to zero out the value after sending any one input command.
3. This MMF will be constantly polled and the state of each input updated. After initialization these are the only responsibilities of the application.

As you can see from the above description, its not designed around being the most intuitive solution, but hopefully it is the most efficient at keeping track of a large number of values.
As it goes now, it requires a good amount of setup to be abstracted out to a useable point.

I have many goals left to accomplish with this project.

Specific goals: Create a UI to allow a user to load/reload any input map file. It could even go as far as being able to create input maps so it is very user friendly. Also be able to define a specific Window to send the inputs to without having to reload (aka, not a config file).
Most important goal however, is to make it easier to hook into without losing efficiency. This project is only around because of a specific desire for effiency in sending inputs to the operating system, so losing that means losing all purpose for this to exist.