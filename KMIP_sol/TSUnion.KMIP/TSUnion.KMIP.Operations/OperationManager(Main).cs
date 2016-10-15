using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using log4net;
using TSUnion.KMIP.Communication;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Attributes;
using TSUnion.KMIP.Core.CryptographicObjects;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;
using TSUnion.KMIP.DAO;
using TSUnion.KMIP.DAO.MSSql;
using TSUnion.KMIP.Server.Base;

namespace TSUnion.KMIP.Operations
{
    public partial class OperationManager
    {


        private static ILog Log = LogManager.GetLogger(typeof(OperationManager));
        private static IKMIPObjectManager storageManager = null;
        private static ResponseMessage MarkResponseMessageAsSuccess(ResponseMessage responseMessage)
        {           
            responseMessage.Header.TimeStamp = DateTime.Now;
            responseMessage.BatchItem.ResultStatus = ResultStatusType.Success;
            //responseMessage.BatchItem.Operation = OperationType.Create;

            if (responseMessage.BatchItem.ResponsePayload.Attribute == null) 
            {
                responseMessage.BatchItem.ResponsePayload.Attribute = new TemplateAttribute();
                responseMessage.BatchItem.ResponsePayload.Attribute.Items = new Dictionary<string, object>();
            }
            return responseMessage;
        }

        public ResponseMessage GenerateNotParsedErrorResponse()
        {
            return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidMessage, ResultStatusType.OperationFailed, "Message cannot be parsed ");
        }


        public ResponseMessage ProceedRequestMessage(RequestMessage requestMessage) 
        {
            ResponseMessage responseMessage = MessageManager.GenerateStandartResponseMessage();
            if (storageManager == null) 
            {
                storageManager = SheepDogWatcher.GetStorage;
            }
          

            try
            {
                Log.Info("Operation: [" + requestMessage.BatchItem.Operation.ToString() + "] is being initializing on the server...");
                switch (requestMessage.BatchItem.Operation)
                {
                    case TSUnion.KMIP.Core.Enumerators.OperationType.Create:
                        {
                            // Log.Info("Operation: [" + requestMessage.BatchItem.Operation.ToString() + "] was initialized on the server");
                            return Create(requestMessage, ref responseMessage);

                        }
                    case OperationType.CreateKeyPair:
                        {
                            //Log.Info("Operation: [" + requestMessage.BatchItem.Operation.ToString() + "] was initialized on the server");
                            return CreateKeyPair(requestMessage, ref responseMessage);

                        }

                    case OperationType.Register:
                        {
                            return Register(requestMessage, ref responseMessage);

                        }

                    case OperationType.ReKey:
                        {
                            return ReKey(requestMessage, ref responseMessage);

                        }

                    case OperationType.ReKeyPair:
                        {
                            return ReKey(requestMessage, ref responseMessage);

                        }

                    case OperationType.DeriveKey:
                        {
                            return DeriveKey(requestMessage, ref responseMessage);

                        }

                    case OperationType.Certify:
                        {
                            return Certify(requestMessage, ref responseMessage);

                        }
                    case OperationType.ReCertify:
                        {
                            return ReCertify(requestMessage, ref responseMessage);

                        }
                    case OperationType.Locate:
                        {
                            return Locate(requestMessage, ref responseMessage);

                        }

                    case OperationType.Check:
                        {
                            return Check(requestMessage, ref responseMessage);

                        }
                    case OperationType.Get:
                        {
                            return Get(requestMessage, ref responseMessage);

                        }

                    case OperationType.GetAttributes:
                        {
                            return GetAttributes(requestMessage, ref responseMessage);

                        }
                    case OperationType.GetAttributeList:
                        {
                            return GetAttributeList(requestMessage, ref responseMessage);

                        }

                    case OperationType.AddAttribute:
                        {
                            return AddAttribute(requestMessage, ref responseMessage);

                        }
                    case OperationType.ModifyAttribute:
                        {
                            return ModifyAttribute(requestMessage, ref responseMessage);

                        }
                    case OperationType.DeleteAttribute:
                        {
                            return DeleteAttribute(requestMessage, ref responseMessage);

                        }
                    case OperationType.ObtainLease:
                        {
                            return ObtainLease(requestMessage, ref responseMessage);

                        }

                    case OperationType.GetUsageAllocation:
                        {
                            return GetUsageAllocation(requestMessage, ref responseMessage);

                        }


                    case OperationType.Activate:
                        {
                            return Activate(requestMessage, ref responseMessage);

                        }

                    case OperationType.Revoke:
                        {
                            return Revoke(requestMessage, ref responseMessage);

                        }
                    case OperationType.Destroy:
                        {
                            return Destroy(requestMessage, ref responseMessage);

                        }
                    case OperationType.Archive:
                        {
                            return Archive(requestMessage, ref responseMessage);

                        }
                    case OperationType.Recover:
                        {
                            return Recover(requestMessage, ref responseMessage);

                        }
                    case OperationType.Validate:
                        {
                            return Validate(requestMessage, ref responseMessage);

                        }
                    case OperationType.Query:
                        {
                            return Query(requestMessage, ref responseMessage);

                        }

                    case OperationType.Cancel:
                        {
                            return Cancel(requestMessage, ref responseMessage);

                        }

                    case OperationType.Poll:
                        {
                            return Poll(requestMessage, ref responseMessage);

                        }
                    case OperationType.DeleteAll:
                        {
                            return DeleteAll(requestMessage, ref responseMessage);

                        }

                    default:
                        {
                            return MessageManager.GenerateBadResponseMessage(ResultReasonType.OperationNotSupported, ResultStatusType.OperationFailed, "This operation is not supported by this release. Please check update KMIP server...");

                        }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                Log.Error(e.InnerException);
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.GeneralFailure, ResultStatusType.OperationFailed, e.Message);
            }


        }
      

        public ResponseMessage Proceed(RequestMessage requestMessage)
        {
            try
            {
                if (requestMessage.Header.ProtocolVersion.ProtocolVersionMajor != ServerProfile.Instance.Specification.ProtocolVersionMajor)
                {
                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidMessage, ResultStatusType.OperationFailed, "Protocol major version mismatch");
                }


                if (requestMessage.BatchItem == null || requestMessage.BatchItem.Payload == null)
                {
                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.InvalidMessage, ResultStatusType.OperationFailed, "Error parsing batch item or payload within batch item");
                }

                ResponseMessage response = ProceedRequestMessage(requestMessage);

                if (response.Size > requestMessage.Header.MaximumResponseSize)
                {
                    return MessageManager.GenerateBadResponseMessage(ResultReasonType.ResponseTooLarge, ResultStatusType.OperationFailed, "Maximum Response Size has been exceeded");
                }

                return response;

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                Log.Error(e.InnerException);
                return MessageManager.GenerateBadResponseMessage(ResultReasonType.GeneralFailure, ResultStatusType.OperationFailed, e.Message);
            }
        }

       
    }
}
