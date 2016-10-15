This project(KMIP_sol) is an extention of an unstable release found at https://kmip.codeplex.com/
Overridden the Base Objects to suite the specs of KMIP KeyBlock as depicted in 
http://docs.oasis-open.org/kmip/spec/v1.0/os/kmip-spec-1.0-os.html#_Toc262581157 


*Project Description*
OASIS KMIP Server - implementation of KMIP protocol that could be found here http://en.wikipedia.org/wiki/Key_Management_Interoperability_Protocol

General features and architecture usage
Simple client example
Prerequisites
Releases

The Key Management Interoperability Protocol (KMIP) is a communication protocol between key management systems and encryption systems. The KMIP standard effort is governed by the Organization for the Advancement of Structured Information Standards (OASIS).
This project does not follow the standart in scope of low-level TTLV encoding and was implemeneted as application programming interface.
A KMIP server stores and controls Managed Objects, Certificates, and user defined objects. Clients then use the protocol to access these objects subject to a security model that is implemented by the servers. Objects have core Base Object properties such as key length and value, as well as extended Attributes that can include user defined attributes.
