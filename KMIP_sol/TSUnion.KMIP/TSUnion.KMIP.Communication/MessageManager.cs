﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication
{
    public class MessageManager
    {
        private static ILog Log = LogManager.GetLogger(typeof(MessageManager));

        private MessageManager() { }

        private static MessageManager instance;

        public static ResponseMessage GenerateBadResponseMessage(ResultReasonType reason, ResultStatusType status, string message)
        {
            ResponseMessage responseMessage = GenerateStandartResponseMessage();
            responseMessage.BatchItem.ResultReason = reason;
            responseMessage.BatchItem.ResultStatus = status;
            responseMessage.BatchItem.ResultMessage = message;

            Log.Warn("Bad response message generated by server.Details: [" + responseMessage.BatchItem.ResultReason.ToString() + "],[" + responseMessage.BatchItem.ResultStatus.ToString() + "]");
            Log.Warn("Error message: " + responseMessage.BatchItem.ResultMessage);

            return responseMessage;
        }
        public static RequestMessage GenerateStandartRequestMessage()
        {
            RequestMessage message = new RequestMessage();

            RequestHeader header = new RequestHeader();
            header.ProtocolVersion = new Core.Common.ProtocolVersionStructure();
            header.ProtocolVersion.ProtocolVersionMajor = 1;
            header.ProtocolVersion.ProtocolVersionMinor = 1;
            header.TimeStamp = DateTime.Now;
            header.MaximumResponseSize = 65000;
            header.AsynchronousIndicator = false;
            RequestPayload requestPayload = new RequestPayload();

            var batchItem = new RequestBatchItem();

            batchItem.Payload = requestPayload;


            message.Header = header;
            message.BatchItem = batchItem;
            return message;
        }


        public static ResponseMessage GenerateStandartResponseMessage()
        {
            ResponseMessage message = new ResponseMessage();

            ResponseHeader header = new ResponseHeader();
            header.ProtocolVersion = new Core.Common.ProtocolVersionStructure();
            header.ProtocolVersion.ProtocolVersionMajor = 1;
            header.ProtocolVersion.ProtocolVersionMinor = 1;
            header.TimeStamp = DateTime.Now;

            ResponsePayloadType requestPayload = new ResponsePayloadType();

            var batchItem = new ResponseBatchItem();

            batchItem.ResponsePayload = requestPayload;


            message.Header = header;
            message.BatchItem = batchItem;
            return message;
        }

        public static MessageManager GetInstance()
        {
            if (instance == null)
            {
                instance = new MessageManager();
            }
            return instance;
        }

        public IMessageConverter GetMessageHelper(ConverterType type) 
        {
            if (type == ConverterType.Simple) 
            {
                return new SimpleMessageConverter();
            }

            if (type == ConverterType.Native)
            {
                return new NativeMessageConverter();
            }

            if (type == ConverterType.JSON)
            {
                throw new NotImplementedException();
            }

            if (type == ConverterType.SOAP)
            {
                throw new NotImplementedException();
            }

            return null;
        }
    }
}