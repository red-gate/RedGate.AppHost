RedGate AppHost
===============

This ilbrary provides a way to run much of your application out of process, including the UI.

**RedGate.AppHost.Server**: Used in your host application to start the child process and return a handle to the entry point

**RedGate.AppHost.Client**: A stub application that will be started with some arguments including the assembly that contains your application which it will load via reflection.

**RedGate.AppHost.Interfaces**: Contains the interfaces that you need to implement, starting with IOutOfProcessEntryPoint.


Key Types & Control Flow
-----------------------------
This library came out of the need to load multiple versions of a native binary inside a single process. This does not work, so we must load stubs inside the host process and remote in the UI that the native libraries create.


1. IOutOfProcessEntryPoint: The code that you wish to run out of process implements this interface to create the remote UI. It is called by RedGate.AppHost.Client via reflection
1. ISafeChildProcessHandle: The RedGate.AppHost.Client will take your framework element and adapt it into an IRemoteElement that can be sent across the remoting boundary.
1. ChildProcessFactory: This will start the remoting channel, the child process and take the IRemoteElement created in the child process and unwrap it back into a FrameworkElement

Examples
--------
There is an example application. It uses WPF to create the Windows chrome, but then the actual content is loaded externally and remoted in. It also shows how to share some services across the remoting boundary.


###Use Cases

 - Write your application in .NET 4/4.5, and load inside an application that needs to remain .NET 2
 - Write your application to take advantage of 64-bit machines with lots of memory, and load inside a 32 bit process
 - Use native libraries in a safe way that don't conflict with other assemblies in the same app domain
 - Isolate badly behaved components
