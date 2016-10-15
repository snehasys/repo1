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
using TSUnion.KMIP.Core.KeyMaterials;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;
using TSUnion.KMIP.DAO;

namespace TSUnion.KMIP.Operations
{
    public partial class OperationManager
    {


      

        private ResponseMessage Register(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {

            var keyBlock = new KeyBlock<SymmetricKeyMaterial>();
            var cryptoObject = new SymmetricKey(ObjectType.SymmetricKey);
            switch (requestMessage.BatchItem.Payload.Type)
            {

                case ObjectType.SymmetricKey:
                    {
                        if (requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("KeyCryptographicAlgorithm"))
                        {
                            KeyCryptographicAlgorithmType cryptographicAlgorithm = (KeyCryptographicAlgorithmType)Enum.Parse(typeof(KeyCryptographicAlgorithmType), requestMessage.BatchItem.Payload.Attribute.Items["KeyCryptographicAlgorithm"].ToString());
                            switch (cryptographicAlgorithm)
                            {
                                case KeyCryptographicAlgorithmType.AES:
                                    {
                                        if (requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("Key"))
                                        {
                                            keyBlock.Value =new Core.KeyMaterials.SymmetricKeyMaterial();
                                            ((SymmetricKeyMaterial)keyBlock.Value).Key = (byte[])requestMessage.BatchItem.Payload.Attribute.Items["Key"];
                                        }
                                       
                                        break;

                                    }
                                case KeyCryptographicAlgorithmType.Extensions:// if the key is for a proprietary algorithm (say SCS masterkey/k_{ALL})
                                    {
                                        if (requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey("KeyBlock"))
                                        {   //copy the supplied key.
                                            keyBlock = (KeyBlock<SymmetricKeyMaterial>)requestMessage.BatchItem.Payload.Attribute.Items["KeyBlock"];
                                        }
                                        break;

                                    }
                                default:
                                    {
                                        return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "This operation is not supported by this release. Please check update KMIP server...");
                                    }

                            }

                        }
                        else 
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "KeyCryptographicAlgorithm was not provided...");
                        }
                        break;
                    }
                default: //objectkeytype is not a symmetric key
                    {
                        return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "This operation is not supported by this release. Please check update KMIP server...");
                    }
            }
            cryptoObject.Key = keyBlock;
            Guid objectId = storageManager.InsertObject(cryptoObject);


            responseMessage = MarkResponseMessageAsSuccess(responseMessage);
            responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(objectId);
            return responseMessage;
        }
    }
}
