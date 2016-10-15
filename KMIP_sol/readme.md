This project(KMIP_sol) is an extention of an unstable release found at https://kmip.codeplex.com/ .
A very basic C# solution for the KMIP 1.0 specification protocol. 
It is distributed under GPLv3 license. 

This solution contains the core API’s for KMIP protocol, a KMS-Server and a test client.
---------------------------------------------------------------------

Overridden the Base Objects to suite the specs of KMIP KeyBlock as depicted in 
http://docs.oasis-open.org/kmip/spec/v1.0/os/kmip-spec-1.0-os.html#_Toc262581157 

Many missing operations have been implemented in its API 
  e.g. Locate, Destroy, ReKey, Validate(more testing required), Check etc.
  So when these operations are called "NotImplementedException()" is not thrown anymore.

Resolved several existing limitation in its Register operation (but it currently only supports AES). 

___________________________________________________________________________
*Useful Tips to perform Inter process communication (say between a c# binary & c++ binary):

To make a C# project APIs callable from a C++ project we need to expose the desired APIs via COM interop objects. (The Component Object Model (COM) allows an object to expose its functionality to other components and to host applications.)
To successfully call a C# method inside a C++ project do the following:

	(1)	First expose the public methods inside public interfaces from the c# project. 

	(2)	Now build the C# dll successfully. 

	(3)	Goto the project’s AssemblyInfo.cs file and add the line
		 [assembly: ComVisible(true)]
 
	(4)	If want to create strong name for your class library go to step (a). 
		Otherwise, jump to step (5).

	     (a)	To create a strong name for your class library, type the following command at the Visual Studio .NET command prompt:
        		 sn.exe -k MyKeyFile.SNK

	     (b)	Copy the file MyKeyFile.SNK to your C# project folder.
     
	     (c)	Add/replace these lines of code in the AssemblyInfo.cs file
				[assembly: ComVisible(false)]
				[assembly: AssemblyDelaySign(false)]
				[assembly: AssemblyKeyFile("")]
			with the following:
				[assembly: ComVisible(true)] 
				[assembly: AssemblyDelaySign(false)] 
				[assembly: AssemblyKeyFile("..\\..\\MyKeyFile.SNK")]
	     (d)	Go to step (5).

	(5)	Rebuild the c# library. (suppose the generated dll name is cSharp.dll)

	(6)	After creating the C# dll, we need the register assembly utility. For that, run the following command from admin console:
         	C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe cSharp.dll  /tlb:cSharp.tlb  /codebase

	(7)	Then we import the generated TLB file path (cSharp.tlb) in our C++ project.

	(8)	Now we can use the defined namespaces of C# module. Also we can access the public methods of C# module via interface pointers in C++ code.

_____________________________________________________________________________________________________________________
*Project Description*

OASIS KMIP Server - implementation of KMIP protocol that could be found here http://en.wikipedia.org/wiki/Key_Management_Interoperability_Protocol
General features and architecture usage
Simple client example
Prerequisites
Releases

The Key Management Interoperability Protocol (KMIP) is a communication protocol between key management systems and encryption systems. The KMIP standard effort is governed by the Organization for the Advancement of Structured Information Standards (OASIS).
This project does not follow the standart in scope of low-level TTLV encoding (binary format consisting of nested Tag, Type, Length and Value) and was implemeneted as application programming interface.
A KMIP server stores and controls Managed Objects, Certificates, and user defined objects. Clients then use the protocol to access these objects subject to a security model that is implemented by the servers. Objects have core Base Object properties such as key length and value, as well as extended Attributes that can include user defined attributes.


_____________________________________________________________________________________________________________________
*Some Extra Gyan (compressed)*

	KMS generally follows the client-server architecture. Hence, the communication between the client and server needs some kind of standardized communication protocol. This section describes such a standard communication protocol, known as KMIP.
	About KMIP
	Key Management Interoperability Protocol (KMIP) is a communication protocol between key management systems and encryption systems. KMIP defines the communication between a Key Lifecycle Management System (KLMS) and its clients.

	KMIP supports legacy and new cryptographic-enabled applications. It supports symmetric keys, asymmetric keys, digital certificates, opaque objects and other "shared secrets." 

	The major operations supported by KMIPv1.0 are as follows:
	
		1>Create - creates a new symmetric key, and return the unique identifier.
		2>Create Key Pair - creates new asymmetric keys, and return the unique identifier.
		3>Get - Retrieves an object's value with respect to its unique identifier.
		4>Register - Stores externally generated key value, and return the unique identifier.
		5>Add Attributes - Manipulates the attributes of a managed object, and return the unique identifier.
		6>Get Attributes- Manipulates the attributes of a managed object, and returns the unique identifier.
		7>Modify Attributes - Manipulates the attributes of a managed object, and returns the unique identifier.
		8>Locate - Retrieves a list of objects based on a conjunction of predicates.
		9>Re-Key - Creates a new key that can replace an existing key.
		10>(Re-)Certify - certifies a certificate.
