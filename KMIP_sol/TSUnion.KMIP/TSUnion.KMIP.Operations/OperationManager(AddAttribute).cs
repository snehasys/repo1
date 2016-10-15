using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TSUnion.KMIP.Communication;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Attributes;
using TSUnion.KMIP.Core.CryptographicObjects;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;
using TSUnion.KMIP.DAO;

namespace TSUnion.KMIP.Operations
{
    public partial class OperationManager
    {




        private ResponseMessage AddAttribute(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

           
            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required UniqueIdentifier attribute to be provided");
            }

            if (requestMessage.BatchItem.Payload.Attribute.AttributeIndex!=0)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidField, ResultStatusType.OperationFailed, "New attribute contains Attribute Index");
            }

       

          

           
            BaseObject existedManagedObject = (BaseObject)storageManager.GetObject(((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]));

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("AttributeName"))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required [AttributeName] attribute to be provided");
            }


            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("AttributeValue"))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required [AttributeValue] attribute to be provided");
            }


            string attributeName = requestMessage.BatchItem.Payload.Attribute.Items["AttributeName"].ToString();
            string attributeValue = requestMessage.BatchItem.Payload.Attribute.Items["AttributeValue"].ToString();

            if (existedManagedObject.IsAttributeExist(attributeName))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.IllegalOperation, ResultStatusType.OperationFailed, "The specified attribute already exists ");
            }
            else
            {
                if (requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("AttributeReadOnly"))
                {
                    bool readOnly = (bool)requestMessage.BatchItem.Payload.Attribute.Items["AttributeReadOnly"];
                    existedManagedObject.AddAttribute(attributeName, attributeValue, false, true);

                }
                else 
                {
                    existedManagedObject.AddAttribute(attributeName, attributeValue);
                }
               
                storageManager.UpdateObject( existedManagedObject);

                responseMessage = MarkResponseMessageAsSuccess(responseMessage);

                responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(existedManagedObject.Id);
                responseMessage.BatchItem.ResponsePayload.Attribute.Items.Add(attributeName, existedManagedObject.Attributes[attributeName]);
                return responseMessage;
            }
        }
    }
}
