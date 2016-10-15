using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class BaseObject
    {

        
        public const int STORAGE_TYPE = 0x00000007;


     
        private ManagedObjectState internalState;
        private DateTime compromiseOccurrenceDate;
        private RevocationReason revocationReason;
        private DateTime compromiseDate;
        private DateTime expiredDate;
        private DateTime deactivationDate;
        private Guid id;
        private String objectGroup;
        private LinkObjectType linkObjectType;
        private Guid linkId;
        private string applicationData;
        private string applicationNamespace;
        private string contactInformation;
        private Dictionary<string, AttributeItem> customAttributes;
        private ObjectType objType;

        public string ContactInformation
        {
            get { return contactInformation; }
            set { contactInformation = value; }
        }

        public ObjectType ObjectType
        {
            get { return objType; }
           
        }

        public string ApplicationData
        {
            get { return applicationData; }
            set { applicationData = value; }
        }
        

        public string ApplicationNamespace
        {
            get { return applicationNamespace; }
            set { applicationNamespace = value; }
        }

        


        public String ObjectGroup
        {
            get { return objectGroup; }
            set { objectGroup = value; }
        }
        private Dictionary<string, AttributeItem> attributes;

        public ManagedObjectState State
        {
            get { return internalState; }
        }

        public void SetLinkObject(LinkObjectType linkType, Guid link) 
        {
            linkObjectType = linkType;
            linkId = link;

        }

        public Guid GetLinkObjectId() 
        {
             return linkId; 
          
        }

        public void RemoveLinkObject()
        {
            
            linkId = Guid.Empty ;

        }

        public string GetAttributeList() 
        {
            if (Attributes != null)
            {
                return Attributes.Keys.Aggregate<string>((x, y) => x + ";" + y);
            }

            else return "";
        }
        private object key;
        public object Key
        {
            get { return key; }
            set
            {

                key = value;
                AddAttribute("Digest", new Digest());
                this.DigestValue.KeyFormat = ((KeyBlockBase)key).Format;
                this.DigestValue.Algorithm = HashingAlgorithmType.SHA_256;
                object keyMaterial = key.GetType().GetProperty("Value").GetValue(key, null);
                this.DigestValue.Value = DigectHelper.GetSHA256DigestOfObject(keyMaterial);


            }
        }
       
        //PreActived,
        //Actived,
        //Deactivated,
        //Compromised,
        //Destroyed,        //DestroyedCompromised
        public void Activate() 
        {

            if (internalState == ManagedObjectState.Actived) 
            {
                throw new Exception("This MO is already activated");
            }
            internalState = ManagedObjectState.Actived;
            ActivationDate = DateTime.Now;
        }

        public void Destroy()
        {
            internalState = ManagedObjectState.Destroyed;
            Key = null;
        }
        public void DestroyedCompromise()
        {
            internalState = ManagedObjectState.DestroyedCompromised;
        }

        public void Deactivate(DateTime compromisedOccurenceDate)
        {
            this.compromiseOccurrenceDate = compromisedOccurenceDate;
            Deactivate();
        }

        public void Deactivate()
        {
            internalState = ManagedObjectState.Deactivated;
            deactivationDate = DateTime.Now;
        }

       

        public DateTime DeactivationDate
        {
            get { return deactivationDate; }
            set { deactivationDate = value; }
        }

        public void Compromise(DateTime compromisedOccurenceDate)
        {
            this.compromiseOccurrenceDate = compromisedOccurenceDate;
            Compromise();
        }
        public void Compromise()
        {
            internalState = ManagedObjectState.Compromised;
            compromiseDate = DateTime.Now;
        }

        public BaseObject(ObjectType objType, DateTime expiredDate) : base() 
        {
            this.expiredDate = expiredDate;
            this.objType = objType;
        }
        public BaseObject(ObjectType objType,DateTime expiredDate, DateTime stopProtectDate) : base() 
        {
            this.expiredDate = expiredDate; this.protectStopDate = stopProtectDate;
            this.objType = objType;
        }
        public BaseObject(ObjectType objType) 
        {
            this.internalState = ManagedObjectState.PreActived;
            this.attributes = new Dictionary<string, AttributeItem>();
            this.objType = objType;
        }


        private void SetRevocationReason(RevocationReason reason) 
        {
            revocationReason = reason;
        }

        public void Revoke(RevocationReason reason) 
        {
            SetRevocationReason(reason);
            if (reason.Type == Enumerators.RevocationReasonType.KeyCompromise) 
            {
                compromiseOccurrenceDate = DateTime.Now;
            }

            else { throw new NotImplementedException("Revoke operation not yet implemented for reason:" + reason.Type.ToString()); }
        }

        public DateTime ExpiredDate
        {
            get { return expiredDate; }
            set { expiredDate = value; }
        }

      


        public DateTime CompromiseDate
        {
            get { return compromiseDate; }
            set { compromiseDate = value; }
        }

      

        public DateTime CompromiseOccurrenceDate
        {
            get { return compromiseOccurrenceDate; }
            set { compromiseOccurrenceDate = value; }
        }

       


        public Dictionary<string, AttributeItem> Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }

       

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name 
        {
            get 
            {
                if (attributes == null)
                {
                    attributes = new Dictionary<string, AttributeItem>();
                }


                if (attributes.ContainsKey("Name"))
                {
                    return attributes["Name"].Value.ToString();
                }
                else
                {
                    throw new KMIPNotFoundNameException("[Name] attribute not found");
                }
            }

            set 
            {
                if (attributes == null)
                {
                    attributes = new Dictionary<string, AttributeItem>();
                }
                if(!attributes.ContainsKey("Name"))
                attributes.Add("Name",new AttributeItem(value));
            }
        }

//        This is the date and time when a Managed Symmetric Key Object MAY begin to be used to process 
// cryptographically-protected information (e.g., decryption or unwrapping), depending on the value of its 
// Cryptographic Usage Mask attribute. The object SHALL NOT be used for these cryptographic purposes 
// before the Process Start Date has been reached. This value MAY be equal to or later than, but SHALL 
// NOT precede, the Activation Date. Once the Process Start Date has occurred, then this attribute SHALL 
// NOT be changed or deleted before the object is destroyed. 
        public DateTime ProcessStartDate
        {
            get
            {
               return (DateTime)GetAttribute("ProcessStartDate");
            }

            set
            {
                SetAttribute("ProcessStartDate", value);
            }
        }

        public DateTime ProcessStopDate
        {
            get
            {
                return (DateTime)GetAttribute("ProcessStopDate");
            }
            set
            {
                SetAttribute("ProcessStopDate", value);
            }
        }

        public  Digest DigestValue
        {
            get
            {
                return (Digest)GetAttribute("Digest");
            }
            set
            {
                SetAttribute("Digest", value);
            }
        }


        public string OperationPolicyName
        {
            get
            {
                return (string)GetAttribute("OperationPolicyName");
            }
            set
            {
                SetAttribute("OperationPolicyName", value);
            }
        }

        public DateTime ActivationDate
        {

            get
            {
                return (DateTime)GetAttribute("ActivationDate");
            }
            set
            {
                SetAttribute("ActivationDate", value);
            }
        }

        public DateTime DestroyDate
        {
            get
            {
                return (DateTime)GetAttribute("DestroyDate");
            }
            set
            {
                SetAttribute("DestroyDate", value);
            }
        }

        public CryptographicUsageMask CryptoUsageMask
        {
            get
            {
                return (CryptographicUsageMask)GetAttribute(Constants.CRYPTO_USAGE_MASK);
            }
            set
            {
                SetAttribute(Constants.CRYPTO_USAGE_MASK, new AttributeItem(value));
            }
        }

        public object GetAttribute(string attribName) 
        {
            if (attribName.StartsWith("x-")) 
            {
                if (customAttributes == null)
                {
                    customAttributes = new Dictionary<string, AttributeItem>();
                }


                if (customAttributes.ContainsKey(attribName))
                {
                    return customAttributes[attribName].Value;
                }
                else
                {
                    throw new KMIPNotFoundAttributeException("Attribute " + attribName + " was not found in the collection");
                }
            }
            else
            {
                if (attributes == null)
                {
                    attributes = new Dictionary<string, AttributeItem>();
                }
                if (attributes.ContainsKey(attribName))
                {
                    return attributes[attribName].Value;
                }
                else
                {
                    throw new KMIPNotFoundAttributeException("Attribute " + attribName + " was not found in the collection");
                }
            }
        
        }

        public void SetAttribute(string attribName, object value)
        {
            if (attribName.StartsWith("x-")) 
            {
                if (customAttributes == null)
                {
                    customAttributes = new Dictionary<string, AttributeItem>();
                }

                if (customAttributes.ContainsKey(attribName))
                {
                    customAttributes[attribName].Value = value;
                }
            }
            else
            {

                if (attributes == null)
                {
                    attributes = new Dictionary<string, AttributeItem>();
                }


                if (attributes.ContainsKey(attribName))
                {
                    attributes[attribName].Value = value;
                }
                else
                {
                    throw new Exception("Such attribute does not exist...");

                }
            }

        }

        public void AddAttribute(string attribName, object value)
        {
            AddAttribute(attribName, value, false, false);
        }
        public void AddAttribute(string attribName, object value, bool required , bool readOnly ) 
        {
            AttributeItem item = new AttributeItem();
            item.Value = value;
            item.Required = required;
            item.ReadOnly = readOnly;
           
            if (attribName.StartsWith("x-")) 
            {


                if (customAttributes == null)
                {
                    customAttributes = new Dictionary<string, AttributeItem>();
                }
                if (!customAttributes.ContainsKey(attribName))
                customAttributes.Add(attribName, item);
            }
            else
            {
                if (attributes == null)
                {
                    attributes = new Dictionary<string, AttributeItem>();
                }
                if (!attributes.ContainsKey(attribName))
                attributes.Add(attribName, item);
            }
        }

        public bool IsAttributeExist(string attribName)
        {
            if (attributes == null) 
            {
                attributes = new Dictionary<string, AttributeItem>();
            }
           return attributes.ContainsKey(attribName);
        }


        public static void Create() 
        {
            throw new NotImplementedException();
        }

        public static void Register(BaseObject obj)
        {
            throw new NotImplementedException();
        }

        public static BaseObject Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public static BaseObject Locate(string query)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAttribute(string attributeName)
        {
            if (attributes == null)
            {
                attributes = new Dictionary<string, AttributeItem>();
                return false;
            }
            if (attributes.ContainsKey(attributeName))
            {
                attributes.Remove(attributeName);
                return true;

            }
            else 
            {
                return false;
            }
        }

        public bool ModifyAttribute(string attributeName, string attributeValue)
        {
            if (attributes == null)
            {
                attributes = new Dictionary<string, AttributeItem>();
                return false;
            }
            if (attributes.ContainsKey(attributeName))
            {
                attributes[attributeName].Value = attributeValue;
                return true;

            }
            else
            {
                return false;
            }




        }

        DateTime protectStopDate;

        public DateTime ProtectStopDate
        {
            get { return protectStopDate; }
            set { protectStopDate = value; }
        }
        public double GetLeaseTimeInSeconds()
        {
            if (expiredDate > DateTime.Now) { return (expiredDate - DateTime.Now).TotalSeconds; }
            else { return 0; }
        }

        public bool IsKey()
        {
            return objType == ObjectType.PublicKey || objType == ObjectType.PrivateKey || objType == ObjectType.SymmetricKey || objType == ObjectType.SplitKey ? true : false;
        }

        public bool IsAttributeReadOnly(string attributeName)
        {
            if (attributes.ContainsKey(attributeName))
            {
                return attributes[attributeName].ReadOnly;
           
            }
            return false;
        }
    }
}
