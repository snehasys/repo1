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


      

        private ResponseMessage Certify(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required UniqueIdentifier attribute to be provided");
            }


            BaseObject existedManagedObject = storageManager.GetBaseObject(((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]));

            if (existedManagedObject == null)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.ItemNotFound, ResultStatusType.OperationFailed, "Object does not exist");
            }

            if (!existedManagedObject.IsKey())
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.IllegalOperation, ResultStatusType.OperationFailed, "Object with Encryption Key Information exists, but it is not a key");
            }

            if (!(existedManagedObject.ObjectType==ObjectType.PublicKey))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.IllegalOperation, ResultStatusType.OperationFailed, "Object with Encryption Key Information exists, but it is not a Public Key");
            }




            throw new NotImplementedException();
        }
    }
}
