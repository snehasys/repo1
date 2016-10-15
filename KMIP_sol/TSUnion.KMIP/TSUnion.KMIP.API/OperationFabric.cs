using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Attributes;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;



namespace TSUnion.KMIP.API
{
    public partial class OperationFactory
    {
        public static RequestMessage ReKey()
        {
            return new RequestMessage();
        }
        public static RequestMessage CreateKeyPair() 
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
            var privateKeyAttribute = new PrivateKeyTemplateAttribute();
            var publicKeyAttribute = new PublicKeyTemplateAttribute();



            //  attribute.Attributes.Add("PrivateKeyAttributes", privateKeyAttribute);
            // attribute.Attributes.Add("PublicKeyAttributes", publicKeyAttribute);
            message.BatchItem.Payload.Attribute = attribute;
            // message.BatchItem.RequestPayload.Type = Core.Enumerators.ObjectType.PrivateKey | Core.Enumerators.ObjectType.PublicKey;
            message.BatchItem.Operation = Core.Enumerators.OperationType.CreateKeyPair;
            return message;
        }


        public static RequestMessage Register(ObjectType type, Dictionary<string,object> parameters)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

                var attribute = new TemplateAttribute();

                attribute.Items = parameters;
                message.BatchItem.Payload.Attribute = attribute;
                message.BatchItem.Payload.Type = type;
              
                message.BatchItem.Operation = Core.Enumerators.OperationType.Register;
                return message;
        }

        public static RequestMessage Derive(Guid id,KeyCryptographicAlgorithmType cryptoType, int cryptoLength,DerivationMethodType derivationType,DerivationParameters parameters)
        {

            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();

          

            attribute.Items.Add(Constants.UID, id);
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, cryptoType);
            attribute.Items.Add("CryptographicLength", cryptoLength);
            attribute.Items.Add("DerivationMethod", derivationType);
            attribute.Items.Add("DerivationParameters", parameters);
  
     
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Operation = Core.Enumerators.OperationType.DeriveKey;
            return message;

        }

        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType,CryptographicUsageMask usageMask,ObjectType objType)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add("Name", System.Environment.MachineName.ToUpper() + "_" + objType.ToString().ToUpper() + "_" + algoritmType.ToString().ToUpper());
            message.BatchItem.Payload.Type = objType;
            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }

        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType, CryptographicUsageMask usageMask, ObjectType objType, RequestMessage message)
        {
            
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add(Constants.NAME, System.Environment.MachineName.ToUpper() + "_" + objType.ToString().ToUpper() + "_" + algoritmType.ToString().ToUpper());
            message.BatchItem.Payload.Type = objType;
            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }

        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType, CryptographicUsageMask usageMask, ObjectType objType,DateTime destroyDate)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
           attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            attribute.Items.Add(Constants.DESTROY_DATE, destroyDate);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add(Constants.NAME, System.Environment.MachineName.ToUpper() + "_" + objType.ToString().ToUpper() + "_" + algoritmType.ToString().ToUpper());
            message.BatchItem.Payload.Type = objType;
            
            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }


        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType, CryptographicUsageMask usageMask, ObjectType objType, DateTime destroyDate, DateTime deactivationDate)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            attribute.Items.Add(Constants.DESTROY_DATE, destroyDate);
            attribute.Items.Add(Constants.DEACTIVATION_DATE, deactivationDate);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add(Constants.NAME, System.Environment.MachineName.ToUpper() + "_" + objType.ToString().ToUpper() + "_" + algoritmType.ToString().ToUpper());
            message.BatchItem.Payload.Type = objType;

            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }


        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType, CryptographicUsageMask usageMask, ObjectType objType, DateTime destroyDate, DateTime deactivationDate, string objectGroup)
        {
            if (String.IsNullOrEmpty(objectGroup)) 
            {
                throw new Exception("ObjectGroup is null or empty...");
            }
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            attribute.Items.Add(Constants.DESTROY_DATE, destroyDate);
            attribute.Items.Add(Constants.DEACTIVATION_DATE, deactivationDate);
            attribute.Items.Add(Constants.OBJ_GROUP, objectGroup);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add(Constants.NAME, System.Environment.MachineName.ToUpper() + "_" + objType.ToString().ToUpper() + "_" + algoritmType.ToString().ToUpper());
            message.BatchItem.Payload.Type = objType;

            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }


        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType, CryptographicUsageMask usageMask, ObjectType objType, DateTime destroyDate, DateTime deactivationDate, string objectGroup,string contactInformation)
        {
            if (String.IsNullOrEmpty(objectGroup))
            {
                throw new Exception("ObjectGroup is null or empty...");
            }
            if (String.IsNullOrEmpty(contactInformation))
            {
                throw new Exception("Contact Information is null or empty...");
            }
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            attribute.Items.Add(Constants.DESTROY_DATE, destroyDate);
            attribute.Items.Add(Constants.DEACTIVATION_DATE, deactivationDate);
            attribute.Items.Add(Constants.OBJ_GROUP, objectGroup);
            attribute.Items.Add(Constants.CONTACT_INFO, contactInformation);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add(Constants.NAME, System.Environment.MachineName.ToUpper() + "_" + objType.ToString().ToUpper() + "_" + algoritmType.ToString().ToUpper());
            message.BatchItem.Payload.Type = objType;

            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }


        public static RequestMessage Create(KeyCryptographicAlgorithmType algoritmType, CryptographicUsageMask usageMask, ObjectType objType, DateTime destroyDate, DateTime deactivationDate, string objectGroup, string contactInformation, string name)
        {
            if (String.IsNullOrEmpty(objectGroup))
            {
                throw new Exception("ObjectGroup is null or empty...");
            }
            if (String.IsNullOrEmpty(contactInformation))
            {
                throw new Exception("Contact Information is null or empty...");
            }
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.CRYPTO_ALGORITHM, algoritmType);
            attribute.Items.Add(Constants.CRYPTO_USAGE_MASK, usageMask);
            attribute.Items.Add(Constants.DESTROY_DATE, destroyDate);
            attribute.Items.Add(Constants.DEACTIVATION_DATE, deactivationDate);
            attribute.Items.Add(Constants.OBJ_GROUP, objectGroup);
            attribute.Items.Add(Constants.CONTACT_INFO, contactInformation);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Payload.Attribute.Items.Add(Constants.NAME, name);
            message.BatchItem.Payload.Type = objType;

            message.BatchItem.Operation = Core.Enumerators.OperationType.Create;
            return message;
        }
        public static RequestMessage Revoke(Guid id, RevocationReason reason)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add("RevocationReason", reason);
            
            message.BatchItem.Payload.Attribute = attribute;
            
            //message.BatchItem.Payload.Type = Core.Enumerators.ObjectType.PrivateKey;
            message.BatchItem.Operation = Core.Enumerators.OperationType.Revoke;
            return message;
        }

        public static RequestMessage Revoke(Guid id, RevocationReason reason, DateTime compromiseOccurrenceDate)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.REVOCATION_REASON, reason);
            attribute.Items.Add("CompromiseOccurrenceDate", compromiseOccurrenceDate);
            message.BatchItem.Payload.Attribute = attribute;

            //message.BatchItem.Payload.Type = Core.Enumerators.ObjectType.PrivateKey;
            message.BatchItem.Operation = Core.Enumerators.OperationType.Revoke;
            return message;
        }

        public static RequestMessage AddAttribute(Guid id, string attrName, string attrValue)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();

            attribute.Items.Add(Constants.UID, id);
            attribute.Items.Add("AttributeName", attrName);
            attribute.Items.Add("AttributeValue", attrValue);
            
            message.BatchItem.Payload.Attribute = attribute;
            
           
            message.BatchItem.Operation = Core.Enumerators.OperationType.AddAttribute;
            return message;
        }

        public static RequestMessage AddAttribute(Guid id, string attrName, string attrValue, bool readOnly)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();

            attribute.Items.Add(Constants.UID, id);
            attribute.Items.Add("AttributeName", attrName);
            attribute.Items.Add("AttributeValue", attrValue);
            attribute.Items.Add("AttributeReadOnly", readOnly);

            message.BatchItem.Payload.Attribute = attribute;


            message.BatchItem.Operation = Core.Enumerators.OperationType.AddAttribute;
            return message;
        }

        public static RequestMessage ModifyAttribute(Guid id, string attrName, string attrValue)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();

            attribute.Items.Add(Constants.UID, id);
            attribute.Items.Add("AttributeName", attrName);
            attribute.Items.Add("AttributeValue", attrValue);

            message.BatchItem.Payload.Attribute = attribute;


            message.BatchItem.Operation = Core.Enumerators.OperationType.ModifyAttribute;
            return message;
        }

        public static RequestMessage DeleteAttribute(Guid managedObjectId, string attrName, int attrIndex)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, managedObjectId);
            attribute.Items.Add("AttributeName", attrName);
            attribute.Items.Add("AttributeIndex", attrIndex);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Operation = Core.Enumerators.OperationType.DeleteAttribute;
            return message;
        }

        public static RequestMessage DeleteAttribute(Guid managedObjectId, string attrName)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, managedObjectId);
            attribute.Items.Add("AttributeName", attrName);
            message.BatchItem.Payload.Attribute = attribute;
            message.BatchItem.Operation = Core.Enumerators.OperationType.DeleteAttribute;
            return message;
        }

        public static RequestMessage Get(Guid id, KeyFormatType keyFormat,KeyCompressionType keyCompression)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();

            attribute.Items.Add(Constants.UID, id);
            attribute.Items.Add("KeyFormat", keyFormat);
            attribute.Items.Add("KeyCompression", keyCompression);

            message.BatchItem.Payload.Attribute = attribute;
            // message.BatchItem.Payload.Type = ObjectType.SymmetricKey;           
            message.BatchItem.Operation = Core.Enumerators.OperationType.Get;
            return message;
        }

        public static RequestMessage GetAttributeList(Guid id)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();


            attribute.Items.Add(Constants.UID, id);
            message.BatchItem.Payload.Attribute = attribute;
            // message.BatchItem.Payload.Type = ObjectType.SymmetricKey;

            message.BatchItem.Operation = Core.Enumerators.OperationType.GetAttributeList;
            return message;
        }

        public static RequestMessage Activate(Guid id)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();

            attribute.Items.Add(Constants.UID, id);
         
            message.BatchItem.Payload.Attribute = attribute;
            // message.BatchItem.Payload.Type = ObjectType.SymmetricKey;

            message.BatchItem.Operation = Core.Enumerators.OperationType.Activate;
            return message;
        }

        public static RequestMessage Recover(Guid id)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, id);
          
            message.BatchItem.Payload.Attribute = attribute;
         
            message.BatchItem.Operation = Core.Enumerators.OperationType.Recover;
            return message;
        }

        public static RequestMessage ObtainLease(Guid id)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, id);

            message.BatchItem.Payload.Attribute = attribute;

            message.BatchItem.Operation = Core.Enumerators.OperationType.ObtainLease;
            return message;
        }


        public static RequestMessage Archive(Guid id)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, id);

            message.BatchItem.Payload.Attribute = attribute;

            message.BatchItem.Operation = Core.Enumerators.OperationType.Archive;
            return message;
        }

        public static RequestMessage DeleteAll()
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();

            var attribute = new TemplateAttribute();
           

            message.BatchItem.Payload.Attribute = attribute;

            message.BatchItem.Operation = Core.Enumerators.OperationType.DeleteAll;
            return message;
        }

        public static RequestMessage Get(Guid id)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, id);
            message.BatchItem.Payload.Attribute = attribute;
            // message.BatchItem.Payload.Type = ObjectType.SymmetricKey;           
            message.BatchItem.Operation = Core.Enumerators.OperationType.Get;
            return message;
        }

        public static RequestMessage GetAttributes(Guid id,string attrName)
        {
            RequestMessage message = TSUnion.KMIP.Communication.MessageManager.GenerateStandartRequestMessage();
            var attribute = new TemplateAttribute();
            attribute.Items.Add(Constants.UID, id);
            attribute.Items.Add("AttributeName", attrName);
            message.BatchItem.Payload.Attribute = attribute;
            // message.BatchItem.Payload.Type = ObjectType.SymmetricKey;           
            message.BatchItem.Operation = Core.Enumerators.OperationType.GetAttributes;
            return message;
        }
    }
}
