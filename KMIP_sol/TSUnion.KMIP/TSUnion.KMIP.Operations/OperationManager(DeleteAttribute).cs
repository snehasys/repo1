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




        private ResponseMessage DeleteAttribute(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required UniqueIdentifier attribute to be provided");
            }

    

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("AttributeName"))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required [AttributeName] attribute to be provided");
            }

            if (storageManager.IsArchived((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.ObjectArchived, ResultStatusType.OperationFailed, "Object is archived");
            }


            BaseObject existedManagedObject = (BaseObject)storageManager.GetObject(((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]));
            string attributeName = requestMessage.BatchItem.Payload.Attribute.Items["AttributeName"].ToString();


            if (!existedManagedObject.IsAttributeExist(attributeName))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.ItemNotFound, ResultStatusType.OperationFailed, "No attribute with the specified name exists");
            }
            else if (existedManagedObject.IsAttributeReadOnly(attributeName)) 
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.PermissionDenied, ResultStatusType.OperationFailed, "Attemp to delete a read-only attribute");
            }
            else
            {
                existedManagedObject.DeleteAttribute(attributeName);
                storageManager.UpdateObject(existedManagedObject);
                responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(existedManagedObject.Id);
                return responseMessage;

            }
        }
    }
}
