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




        private ResponseMessage GetAttributes(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required UniqueIdentifier attribute to be provided");
            }


            if (storageManager.IsArchived((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.ObjectArchived, ResultStatusType.OperationFailed, "Object is archived");
            }



            BaseObject existedManagedObject = (BaseObject)storageManager.GetObject(((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]));



            if (existedManagedObject == null)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.ItemNotFound, ResultStatusType.OperationFailed, "No  object with the specified Unique Identifier exists");
            }

            String attribName = requestMessage.BatchItem.Payload.Attribute.Items["AttributeName"].ToString();
            responseMessage = MarkResponseMessageAsSuccess(responseMessage);
            var attribValue = existedManagedObject.GetAttribute(attribName);
            responseMessage.BatchItem.ResponsePayload.Attribute.Items.Add(attribName, attribValue.ToString());
            responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(existedManagedObject.Id);
            return responseMessage;
        }
    }
}
