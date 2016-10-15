using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    [Serializable]
    public class Certificate : ManagedCryptographicObject
    {


        public Certificate(ObjectType objType, DateTime expiredDate) : base(objType,expiredDate) { }
         public Certificate(ObjectType objType) : base(objType,DateTime.Now.AddMonths(1)) { }
    

        public const int STORAGE_TYPE = 0x00000001;

        public CertificateType CertificateType
        {
            get
            {
                return (CertificateType)GetAttribute("CertificateType");
            }
            set
            {
                SetAttribute("CertificateType", value);
            }
        }


        public int CertificateLength
        {
            get
            {
                return (int)GetAttribute("CertificateLength");
            }
            set
            {
                SetAttribute("CertificateLength", value);
            }
        }


        public CertificateIdentifier CertificateIdentifier
        {
            get
            {
                return (CertificateIdentifier)GetAttribute("CertificateIdentifier");
            }
            set
            {
                SetAttribute("CertificateIdentifier", value);
            }
        }


        public CertificateSubject CertificateSubject
        {
            get
            {
                return (CertificateSubject)GetAttribute("CertificateSubject");
            }
            set
            {
                SetAttribute("CertificateSubject", value);
            }
        }

        public CertificateIssuer CertificateIssuer
        {
            get
            {
                return (CertificateIssuer)GetAttribute("CertificateIssuer");
            }
            set
            {
                SetAttribute("CertificateIssuer", value);
            }
        }





        public X509CertificateIdentifier X509CertificateIdentifier
        {
            get
            {
                return (X509CertificateIdentifier)GetAttribute("X509CertificateIdentifier");
            }
            set
            {
                SetAttribute("X509CertificateIdentifier", value);
            }
        }


        public X509CertificateSubject X509CertificateSubject
        {
            get
            {
                return (X509CertificateSubject)GetAttribute("X509CertificateSubject");
            }
            set
            {
                SetAttribute("X509CertificateSubject", value);
            }
        }

        public X509CertificateIssuer X509CertificateIssuer
        {
            get
            {
                return (X509CertificateIssuer)GetAttribute("X509CertificateIssuer");
            }
            set
            {
                SetAttribute("X509CertificateIssuer", value);
            }
        }

        public DigitalSignatureAlgorithmType DigitalSignatureAlgorithm
        {
            get
            {
                return (DigitalSignatureAlgorithmType)GetAttribute("DigitalSignatureAlgorithm");
            }
            set
            {
                SetAttribute("DigitalSignatureAlgorithm", value);
            }
        }


     
       
    }
}
