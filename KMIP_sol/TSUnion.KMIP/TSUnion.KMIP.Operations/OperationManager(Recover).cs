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




        private ResponseMessage Recover(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required UniqueIdentifier attribute to be provided");
            }



            if (((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]) == Guid.Empty)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required at least 1 UniqueIdentifier item to be provided");
            }




            BaseObject existedManagedObject = storageManager.GetBaseObjectFromArchive(((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]));

            if (existedManagedObject == null)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "The item with ID specified in the request could not be found.");
            }

            storageManager.Recover(existedManagedObject);

            responseMessage = MarkResponseMessageAsSuccess(responseMessage);
            responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(existedManagedObject.Id);
            return responseMessage;
        }
    }
}
