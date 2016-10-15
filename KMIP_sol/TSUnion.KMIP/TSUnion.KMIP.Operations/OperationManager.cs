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


        private ResponseMessage CreateKeyPair(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {
            try
            {
                var keyBlockPublic = new KeyBlock<RSAPublicKeyMaterial>();
                var keyBlockPrivate = new KeyBlock<RSAPrivateKeyMaterial>();

                //------------------------------------------------
                var keyPublic = new PublicKey(ObjectType.PublicKey);
                var keyPrivate = new PrivateKey(ObjectType.PrivateKey);

                PrivateKeyTemplateAttribute privateKeyAttribute = null;
                PublicKeyTemplateAttribute publicKeyAttribute = null;

                Dictionary<string, object> attribs = null;
                if (requestMessage.BatchItem.Payload.Attribute.Items != null)
                {
                    attribs = requestMessage.BatchItem.Payload.Attribute.Items;

                    if (attribs.ContainsKey(Constants.PRIVATE_KEY_ATTRIBUTE)) 
                    {
                        privateKeyAttribute = (PrivateKeyTemplateAttribute)attribs[Constants.PRIVATE_KEY_ATTRIBUTE];
                       
                    }
                    if (attribs.ContainsKey(Constants.PUBLIC_KEY_ATTRIBUTE))
                    {

                        publicKeyAttribute = (PublicKeyTemplateAttribute)attribs[Constants.PUBLIC_KEY_ATTRIBUTE];
                    }
                }


                if (privateKeyAttribute != null) 
                {
                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "PrivateKey attribute not supported");
                
                }
                if (publicKeyAttribute != null) 
                {
                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "PublicKey attribute not supported");
                }

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                //Save the public key information to an RSAParameters structure.
                RSAParameters RSAKeyInfo = RSA.ExportParameters(true);

                keyBlockPrivate.Value = new TSUnion.KMIP.Core.KeyMaterials.RSAPrivateKeyMaterial();
                ((RSAPrivateKeyMaterial)keyBlockPrivate.Value).P = RSAKeyInfo.DP;
                ((RSAPrivateKeyMaterial)keyBlockPrivate.Value).Q = RSAKeyInfo.DQ;
                keyPrivate.Key = keyBlockPrivate;
                Guid objectIdbPrivate = storageManager.InsertObject(keyPrivate);

                keyBlockPublic.Value = new TSUnion.KMIP.Core.KeyMaterials.RSAPublicKeyMaterial();
                ((RSAPublicKeyMaterial)keyBlockPublic.Value).Modulus = RSAKeyInfo.Modulus;
                ((RSAPublicKeyMaterial)keyBlockPublic.Value).PublicExponent = RSAKeyInfo.Exponent;
                keyPublic.Key = keyBlockPublic;
                Guid objectIdbPublic = storageManager.InsertObject(keyPublic);

                responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(objectIdbPrivate);
                responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(objectIdbPublic);

            }
            catch (Exception e) 
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.GeneralFailure, ResultStatusType.OperationFailed, e.Message);
            }
            return responseMessage;
        }
      

       
    }
}
