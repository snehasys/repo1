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




        private ResponseMessage Activate(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

            Guid internalIdToProcess = new Guid() ;

            if (requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                if (((List<Guid>)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]).Count() != 0)
                {

                    if (((List<Guid>)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]).Count() != 1)
                    {

                        return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required only 1 UniqueIdentifier item to be provided");
                    }
                    else
                    {
                        internalIdToProcess = ((List<Guid>)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]).First();
                    }
                }
            }
            else 
            {
                internalIdToProcess = TryToGetPlaceHolderIdFromServer();
            }

            if (internalIdToProcess == new Guid("00000000-0000-0000-0000-000000000000")) 
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required only 1 UniqueIdentifier item to be provided. Neither Placeholder Id, nor requst UniqueIdentifier was provided ");
            }
            BaseObject existedManagedObject = (BaseObject)storageManager.GetObject(internalIdToProcess);



            if (existedManagedObject == null)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.IllegalOperation, ResultStatusType.OperationFailed, " MO could not be found with ID : " + existedManagedObject.Id);
            }
            else
            {

                existedManagedObject.Activate();
                storageManager.UpdateObject( existedManagedObject);

                responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                
                responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(existedManagedObject.Id);
                return responseMessage;

            }
        }

        private Guid TryToGetPlaceHolderIdFromServer()
        {
            return new Guid("00000000-0000-0000-0000-000000000000");
        }

       
    }
}
