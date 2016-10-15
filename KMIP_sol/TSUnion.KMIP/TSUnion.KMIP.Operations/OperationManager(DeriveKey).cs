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
using TSUnion.KMIP.Cryptography.Hash;
using TSUnion.KMIP.DAO;

namespace TSUnion.KMIP.Operations
{
    public partial class OperationManager
    {

        private const int MAX_DERIVE_BYTE_LENGTH = 40000;


        private ResponseMessage DeriveKey(RequestMessage requestMessage, ref ResponseMessage responseMessage)
        {


            var cryptoObject = new SymmetricKey(ObjectType.SymmetricKey);

            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.UID))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required UniqueIdentifier attribute to be provided");
            }






            DerivationMethodType derivationMethodType;
            DerivationParameters derivationParameters = null;
            int cryptoLength = 0;

            BaseObject existedManagedObject = (BaseObject)storageManager.GetObject(((Guid)requestMessage.BatchItem.Payload.Attribute.Items[Constants.UID]));

            if (existedManagedObject == null)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.ItemNotFound, ResultStatusType.OperationFailed, "One or more of the objects specified do not exist");
            }

            if (existedManagedObject.CryptoUsageMask.DeriveKey != true)
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidField, ResultStatusType.OperationFailed, "One or more of the objects specified are not of the correct type ");
            }



            //getting DerivationMethod from request
            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.DERIVIATION_METHOD))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required DerivationMethod to be provided");
            }
            else
            {
                derivationMethodType = (DerivationMethodType)Enum.Parse(typeof(DerivationMethodType), requestMessage.BatchItem.Payload.Attribute.Items["DerivationMethod"].ToString());
            }

            //getting DerivationParameters from request
            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.DERIVIATION_PARMS))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required  attribute item: DerivationParameters to be provided");
            }
            else
            {
                derivationParameters = (DerivationParameters)requestMessage.BatchItem.Payload.Attribute.Items[Constants.DERIVIATION_PARMS];
            }


            if (!requestMessage.BatchItem.Payload.Attribute.Items.ContainsKey(Constants.CRYPTO_LENGTH))
            {
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required  attribute item: Crypto Length to be provided");
            }
            else
            {
                cryptoLength = (int)requestMessage.BatchItem.Payload.Attribute.Items[Constants.CRYPTO_LENGTH];
            }




            switch (derivationMethodType)
            {
                case DerivationMethodType.PBKDF2:
                    {
                        if (derivationParameters.Salt == null || derivationParameters.Salt.Count() == 0)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required  DerivationParameters.Salt to be provided");
                        }
                        if (derivationParameters.Salt.Count() < 8)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidField, ResultStatusType.OperationFailed, "Salt should be not least than 8");
                        }
                        if (derivationParameters.IterationCount == 0)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.MissingData, ResultStatusType.OperationFailed, "This operation required  DerivationParameters.IterationCount to be provided");
                        }
                        KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;
                        Rfc2898DeriveBytes derivedKey = new Rfc2898DeriveBytes(keyBlock.Value.Instance.Key, derivationParameters.Salt, derivationParameters.IterationCount);
                        keyBlock.Value = new SymmetricKeyMaterial();

                        if (cryptoLength > MAX_DERIVE_BYTE_LENGTH)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.CryptographicFailure, ResultStatusType.OperationFailed, "The specified length exceeds the output of the derivation method or other cryptographic error during derivation. ");
                        }

                        try
                        {
                            derivedKey.GetBytes(cryptoLength);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.CryptographicFailure, ResultStatusType.OperationFailed, "The specified length exceeds the output of the derivation method or other cryptographic error during derivation. ");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.CryptographicFailure, ResultStatusType.OperationFailed, "The specified length exceeds the output of the derivation method or other cryptographic error during derivation. ");
                        }
                        catch (Exception)
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.CryptographicFailure, ResultStatusType.OperationFailed, "The specified length exceeds the output of the derivation method or other cryptographic error during derivation. ");
                        }


                        SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                        derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                        ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                        ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = derivedKey.GetBytes(cryptoLength);

                        Guid returnId = storageManager.InsertObject(derivedObj);

                        responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                        responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                        return responseMessage;

                        break;
                    }
                case DerivationMethodType.HASH:
                    {

                        switch (derivationParameters.CryptoParameters.HashingAlgorithm)
                        {
                            case HashingAlgorithmType.SHA_1:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    SHA1 sha = new SHA1CryptoServiceProvider();

                                    byte[] hashedKey = sha.ComputeHash(keyBlock.Value.Instance.Key);

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }

                            case HashingAlgorithmType.SHA_256:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    SHA256 sha = new SHA256CryptoServiceProvider();

                                    byte[] hashedKey = sha.ComputeHash(keyBlock.Value.Instance.Key);

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }

                            case HashingAlgorithmType.SHA_384:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    SHA384 sha = new SHA384CryptoServiceProvider();

                                    byte[] hashedKey = sha.ComputeHash(keyBlock.Value.Instance.Key);

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }

                            case HashingAlgorithmType.SHA_512:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    SHA512 sha = new SHA512CryptoServiceProvider();

                                    byte[] hashedKey = sha.ComputeHash(keyBlock.Value.Instance.Key);

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }
                            case HashingAlgorithmType.MD5:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    MD5 sha = new MD5CryptoServiceProvider();

                                    byte[] hashedKey = sha.ComputeHash(keyBlock.Value.Instance.Key);

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }

                            case HashingAlgorithmType.Whirlpool:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    Whirlpool whirpoolProvider = new Whirlpool();
                                    byte[] resBuf = new byte[whirpoolProvider.GetDigestSize()];
                                    whirpoolProvider.BlockUpdate(keyBlock.Value.Instance.Key, 0, keyBlock.Value.Instance.Key.Length);
                                    whirpoolProvider.DoFinal(resBuf, 0);


                                    byte[] hashedKey = resBuf;

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }

                            case HashingAlgorithmType.Tiger:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    Tiger tigerProvider = new Tiger();
                                    byte[] resBuf = new byte[tigerProvider.GetDigestSize()];
                                    tigerProvider.BlockUpdate(keyBlock.Value.Instance.Key, 0, keyBlock.Value.Instance.Key.Length);
                                    tigerProvider.DoFinal(resBuf, 0);


                                    byte[] hashedKey = resBuf;

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }


                           
                            default:
                                {
                                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "This HASH feature is not supported by this release. Please check and update KMIP server...");
                                }
                        }
                        break;
                    }
                case DerivationMethodType.HMAC:
                    {
                        switch (derivationParameters.CryptoParameters.HashingAlgorithm)
                        {

                            case HashingAlgorithmType.RIPEMD_160:
                                {
                                    KeyBlock<SymmetricKeyMaterial> keyBlock = (KeyBlock<SymmetricKeyMaterial>)existedManagedObject.Key;

                                    RipeMD160 tigerProvider = new RipeMD160();
                                    byte[] resBuf = new byte[tigerProvider.GetDigestSize()];
                                    tigerProvider.BlockUpdate(keyBlock.Value.Instance.Key, 0, keyBlock.Value.Instance.Key.Length);
                                    tigerProvider.DoFinal(resBuf, 0);


                                    byte[] hashedKey = resBuf;

                                    SymmetricKey derivedObj = new SymmetricKey(ObjectType.SymmetricKey);
                                    derivedObj.Key = new KeyBlock<SymmetricKeyMaterial>();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value = new SymmetricKeyMaterial();
                                    ((KeyBlock<SymmetricKeyMaterial>)derivedObj.Key).Value.Instance.Key = hashedKey;

                                    Guid returnId = storageManager.InsertObject(derivedObj);

                                    responseMessage = MarkResponseMessageAsSuccess(responseMessage);
                                    responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.Add(returnId);
                                    return responseMessage;
                                    break;
                                }
                            default:
                                {
                                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "This HASH feature is not supported by this release. Please check and update KMIP server...");
                                }
                        }
                        break;
                    }
                default:
                    {
                        return MessageManager.GenerateBadResponseMessage(ResultReasonType.FeatureNotSupported, ResultStatusType.OperationFailed, "This feature is not supported by this release. Please check and update KMIP server...");

                    }
            }





        }
    }
}
   
