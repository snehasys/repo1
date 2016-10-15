using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum LinkObjectType
    {
        PrivatKeyLink,// For a Public Key object: the private key corresponding to the public key. 
        PublicKeyLink,// For a Private Key object: the public key corresponding to the private key. For a Certificate object: the public key contained in the certificate. 
        CertificateLink,// For Certificate objects: the parent certificate for a certificate in a certificate chain. For Public Key objects: the corresponding certificate(s), containing the same public key. 
        DerivationBaseObjectLink,// for a derived Symmetric Key object: the object(s) from which the current symmetric key was derived. 
        DerivedKeyLink,// the symmetric key(s) that were derived from the current object. 
        ReplacementObjectLink,// For a Symmetric Key object: the key that resulted from the re-key of the current key. For a Certificate object: the certificate that resulted from the re-certify. Note that there SHALL be only one such replacement object per Managed Object. 
        ReplacedObjectLink // For a Symmetric Key object: the key that was re-keyed to obtain the current key. For a Certificate object: the certificate that was re-certified to obtain the current certificate
    }
}
