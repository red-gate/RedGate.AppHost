RedGate AppHost
===============

This ilbrary provides a way to run much of your application out of process, including the UI.

**RedGate.AppHost.Server**: Used in your host application to start the child process and return a handle to the entry point

**RedGate.AppHost.Client**: A stub application that will be started with some arguments including the assembly that contains your application which it will load via reflection.

**RedGate.AppHost.Interfaces**: Contains the interfaces that you need to implement, starting with IOutOfProcessEntryPoint.


Examples
--------
There is an example application. It uses WPF to create the Windows chrome, but then the actual content is loaded externally and remoted in. It also shows how to share some services across the remoting boundary.


###Use Cases

 - Write your application in .NET 4/4.5, and load inside an application that needs to remain .NET 2
 - Write your application to take advantage of 64-bit machines with lots of memory, and load inside a 32 bit process
 - Use native libraries in a safe way that don't conflict with other assemblies in the same app domain
 - Isolate badly behaved components
