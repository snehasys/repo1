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
using TSUnion.KMIP.Cryptography;
using TSUnion.KMIP.DAO;

namespace TSUnion.KMIP.Operations
{
    public partial class OperationManager
    {
        private ResponseMessage Create(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {
            //general initialisation
            try
            {
                var keyBlock = new KeyBlock<SymmetricKeyMaterial>();
                BaseObject cryptoObject ;
                switch (requestMessage.BatchItem.Payload.Type)
                {
                    case ObjectType.SymmetricKey:
                        {
                            cryptoObject = new SymmetricKey(ObjectType.SymmetricKey);
                            break;
                        }

                    default:
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidField, ResultStatusType.OperationFailed, "Object Type is not recognized");
                        }

                }
               
                
                
                Dictionary<string, object> attribs = null;
                if (requestMessage.BatchItem.Payload.Attribute.Items != null)
                {
                    attribs = requestMessage.BatchItem.Payload.Attribute.Items;
                }
                else
                {
                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.GeneralFailure, ResultStatusType.OperationFailed, "There is no Attribute field");
                }




                if (attribs[Constants.CRYPTO_ALGORITHM] != null)
                {
                    KeyCryptographicAlgorithmType cryptographicAlgorithm = (KeyCryptographicAlgorithmType)Enum.Parse(typeof(KeyCryptographicAlgorithmType), attribs[Constants.CRYPTO_ALGORITHM].ToString());
                    //setting Algorithm
                    keyBlock.Algorithm = cryptographicAlgorithm;

                    //proceeeding with request
                   

                                ICryptoProvider provider = CryptoServiceProvider.GetCryptoProvider(cryptographicAlgorithm);
                                if (provider == null) 
                                {

                                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.CryptographicFailure, ResultStatusType.OperationFailed, "Error creating cryptographic object");

                                }

                                keyBlock.Value = new Core.KeyMaterials.SymmetricKeyMaterial();
                                keyBlock.Value.Instance.Key = provider.GetKey();
                                keyBlock.Format = KeyFormatType.TransparentSymmetricKey;
                                keyBlock.Salt = provider.GetSalt();
                                keyBlock.Iterations = provider.GetDefaultIteration();
                                cryptoObject.Key = keyBlock;
                            


                   
                }

                if (attribs.ContainsKey(Constants.DESTROY_DATE) ) 
                {
                    cryptoObject.DestroyDate = (DateTime)attribs[Constants.DESTROY_DATE];
                }

                if (attribs.ContainsKey(Constants.DEACTIVATION_DATE))
                {
                    cryptoObject.DeactivationDate = (DateTime)attribs[Constants.DEACTIVATION_DATE];
                }


                if (attribs.ContainsKey(Constants.OBJ_GROUP))
                {
                    cryptoObject.ObjectGroup = attribs[Constants.OBJ_GROUP].ToString();
                }



                if (attribs.ContainsKey(Constants.NAME))
                {
                    cryptoObject.Name = attribs[Constants.NAME].ToString();
                }


                foreach (var attribute in requestMessage.BatchItem.Payload.Attribute.Items)
                {
                    cryptoObject.AddAttribute(attribute.Key, attribute.Value);
                }
                Guid objectId = storageManager.InsertObject(cryptoObject);


                responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(objectId);


              

            }
            catch (Exception e)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.GeneralFailure, ResultStatusType.OperationFailed, e.Message);
            }
            return responseMessage;

        }

       

 }
}
