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


      

        private ResponseMessage DeleteAll(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

           int removedCount= storageManager.DeleteAllObjects();
            responseMessage = MarkResponseMessageAsSuccess(responseMessage);
            responseMessage.BatchItem.ResponsePayload.Attribute.Items.Add("RemovedCount", removedCount);
            return responseMessage;
        }
    }
}
