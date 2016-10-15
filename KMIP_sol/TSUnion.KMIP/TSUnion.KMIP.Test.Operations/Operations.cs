using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSUnion.KMIP.API;
using TSUnion.KMIP.Communication;
using TSUnion.KMIP.Communication.Transport;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.CryptographicObjects;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Test.Operations
{
    [TestClass]
    public class Operations
    {

        Guid recArchId = new Guid("9DE54FFF-8830-4CA3-A287-DA2FACA6571F");
        Guid addAttr = new Guid("94875322-3651-48E6-B856-CF65C0459950");


        [TestMethod]
        public void AddAttribute()
        {
            string attrName = "PersonOrderKey";
            string attrValue = @"http:\\331\34\3";
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            client.Connect(details, false);

            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
             request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), attrName, attrValue);
            client.SendRequest(request);
             response = client.WaitForResponse();
           
            
            
            client.Close();
            Assert.AreEqual(attrValue, ((AttributeItem)response.BatchItem.ResponsePayload.Attribute.Items[attrName]).Value);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }


        [TestMethod]
        public void ModifyAttribute()
        {
            string attrName = "PersonOrderKey";
            string attrValue = @"http:\\331\34\3\5\7\5\4\4";
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details);

            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            
            request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), attrName, attrValue);
            client.SendRequest(request);
            response = client.WaitForResponse();


            request = OperationFactory.ModifyAttribute(response.GetFirstReturnedId(), attrName, attrValue);
            client.SendRequest(request);
            response = client.WaitForResponse();
            
            client.Close();
            Assert.AreEqual(attrValue,((AttributeItem) response.BatchItem.ResponsePayload.Attribute.Items[attrName]).Value);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void Archive()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

            request = OperationFactory.Archive(response.GetFirstReturnedId());
            client.SendRequest(request);
            response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void ObtainLease()
        {
            Guid id = new Guid("0A20515E-829C-45E8-A48A-1813B7472155");
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details,false);
            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

             request = OperationFactory.ObtainLease(response.GetFirstReturnedId());
            client.SendRequest(request);
             response = client.WaitForResponse();
            client.Close();
            Assert.IsNotNull(response.BatchItem.ResponsePayload.Attribute.Items["LeaseTime"]);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }


       

        [TestMethod]
        public void CreateAESSymmetricKey()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details,false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }


        [TestMethod]
        public void CreateDESSymmetricKeyParallel()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.DES3, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void CreateAESPrivateKey()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.PrivateKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultReasonType.InvalidField, response.BatchItem.ResultReason);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void CreateAESPublicKey()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.PrivateKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultReasonType.InvalidField, response.BatchItem.ResultReason);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void CreateObj_AddAttrib()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();
            RequestMessage requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data");
            
      
            client.SendRequest(requestAddAttrib);
            ResponseMessage responseAddAttrib = client.WaitForResponse();
            
            client.Close();
            Assert.AreEqual(requestAddAttrib.GetId(), responseAddAttrib.GetFirstReturnedId());
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void FCreateObj_AddAttrib2_Same()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();
            
            
            RequestMessage requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data");
            client.SendRequest(requestAddAttrib);
            ResponseMessage responseAddAttrib = client.WaitForResponse();



             requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data");
             client.SendRequest(requestAddAttrib);
             responseAddAttrib = client.WaitForResponse();





             client.Close();
             Assert.AreEqual(ResultReasonType.IllegalOperation, responseAddAttrib.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }



        [TestMethod]
        public void FDeleteReadOnlyAttribute()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();

            RequestMessage requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data",true);
            client.SendRequest(requestAddAttrib);
            ResponseMessage responseAddAttrib = client.WaitForResponse();

            RequestMessage requestDeleteAttrib = OperationFactory.DeleteAttribute(responseCreate.GetFirstReturnedId(), "Order", 1);
            client.SendRequest(requestDeleteAttrib);
            ResponseMessage responseDeleteAttrib = client.WaitForResponse();

            client.Close();
            Assert.AreEqual(ResultReasonType.PermissionDenied, responseDeleteAttrib.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }





        [TestMethod]
        public void FMissingBatchItem()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);

            RequestMessage message = new RequestMessage();

            RequestHeader header = new RequestHeader();
            header.ProtocolVersion = new Core.Common.ProtocolVersionStructure();
            header.ProtocolVersion.ProtocolVersionMajor = 1;
            header.ProtocolVersion.ProtocolVersionMinor = 1;
            header.TimeStamp = DateTime.Now;
            header.MaximumResponseSize = 65000;
            header.AsynchronousIndicator = false;


            message.Header = header;


            client.SendRequest(message);
            ResponseMessage response = client.WaitForResponse();


            client.Close();
            Assert.AreEqual(ResultReasonType.InvalidMessage, response.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }
        
        
        [TestMethod]
        public void FMissingPayload()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);

            RequestMessage message = new RequestMessage();

            RequestHeader header = new RequestHeader();
            header.ProtocolVersion = new Core.Common.ProtocolVersionStructure();
            header.ProtocolVersion.ProtocolVersionMajor = 1;
            header.ProtocolVersion.ProtocolVersionMinor = 1;
            header.TimeStamp = DateTime.Now;
            header.MaximumResponseSize = 65000;
            header.AsynchronousIndicator = false;
           

            var batchItem = new RequestBatchItem();

      
            message.Header = header;
            message.BatchItem = batchItem;

            client.SendRequest(message);
            ResponseMessage response = client.WaitForResponse();


            client.Close();
            Assert.AreEqual(ResultReasonType.InvalidMessage, response.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }

        [TestMethod]
        public void FTooBigResponse()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);

            RequestMessage message = new RequestMessage();

            RequestHeader header = new RequestHeader();
            header.ProtocolVersion = new Core.Common.ProtocolVersionStructure();
            header.ProtocolVersion.ProtocolVersionMajor = 1;
            header.ProtocolVersion.ProtocolVersionMinor = 1;
            header.TimeStamp = DateTime.Now;
            header.MaximumResponseSize = 1;
            header.AsynchronousIndicator = false;
            RequestPayload requestPayload = new RequestPayload();

            var batchItem = new RequestBatchItem();

            batchItem.Payload = requestPayload;


            message.Header = header;
            message.BatchItem = batchItem;
           
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey,message);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();


            client.Close();
            Assert.AreEqual(ResultReasonType.ResponseTooLarge, response.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }
        
        
        [TestMethod]
        public void FSendIncorectVersionMajor()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);

            RequestMessage message = new RequestMessage();

            RequestHeader header = new RequestHeader();
            header.ProtocolVersion = new Core.Common.ProtocolVersionStructure();
            header.ProtocolVersion.ProtocolVersionMajor = 2;
            header.ProtocolVersion.ProtocolVersionMinor = 1;
            header.TimeStamp = DateTime.Now;
            header.MaximumResponseSize = 65000;
            header.AsynchronousIndicator = false;
            RequestPayload requestPayload = new RequestPayload();

            var batchItem = new RequestBatchItem();

            batchItem.Payload = requestPayload;


            message.Header = header;
            message.BatchItem = batchItem;

            client.SendRequest(message);
            ResponseMessage response = client.WaitForResponse();


            client.Close();
            Assert.AreEqual(ResultReasonType.InvalidMessage, response.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }

        [TestMethod]
        public void ArchiveObject()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();

            RequestMessage requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data");
            client.SendRequest(requestAddAttrib);
            ResponseMessage responseAddAttrib = client.WaitForResponse();

            RequestMessage requestArchive = OperationFactory.Archive(responseCreate.GetFirstReturnedId());
            client.SendRequest(requestArchive);
            ResponseMessage responseArchive = client.WaitForResponse();

            //RequestMessage requestDeleteAttrib = OperationFabric.DeleteAttribute(responseCreate.GetFirstReturnedId(), "Order", 1);
            //client.SendRequest(requestDeleteAttrib, Core.MessageHelperType.Simple);
            //ResponseMessage responseDeleteAttrib = client.WaitForResponse(Core.MessageHelperType.Simple);

            client.Close();
            Assert.AreEqual(ResultStatusType.Success, responseArchive.BatchItem.ResultStatus);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }




        [TestMethod]
        public void FDeleteAttributeOfArchiveObject()
        {
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();

            RequestMessage requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data");
            client.SendRequest(requestAddAttrib);
            ResponseMessage responseAddAttrib = client.WaitForResponse();

            RequestMessage requestArchive = OperationFactory.Archive(responseCreate.GetFirstReturnedId());
            client.SendRequest(requestArchive);
            ResponseMessage responseArchive = client.WaitForResponse();

            RequestMessage requestDeleteAttrib = OperationFactory.DeleteAttribute(responseCreate.GetFirstReturnedId(), "Order", 1);
            client.SendRequest(requestDeleteAttrib);
            ResponseMessage responseDeleteAttrib = client.WaitForResponse();

            client.Close();
            Assert.AreEqual(ResultReasonType.ObjectArchived, responseDeleteAttrib.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);
        }



        [TestMethod]
        public void FDeleteNotExistAttribute()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();


            RequestMessage requestAddAttrib = OperationFactory.AddAttribute(responseCreate.GetFirstReturnedId(), "Order", "Data", true);
            client.SendRequest(requestAddAttrib);
            ResponseMessage responseAddAttrib = client.WaitForResponse();



            RequestMessage requestDeleteAttrib = OperationFactory.DeleteAttribute(responseCreate.GetFirstReturnedId(), "Order2", 1);
            client.SendRequest(requestDeleteAttrib);
            ResponseMessage responseDeleteAttrib = client.WaitForResponse();





            client.Close();
            Assert.AreEqual(ResultReasonType.ItemNotFound, responseDeleteAttrib.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }



        [TestMethod]
        public void FGetAttribListOfArchived()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage responseCreate = client.WaitForResponse();


            RequestMessage requestArchive = OperationFactory.Archive(responseCreate.GetFirstReturnedId());
            client.SendRequest(requestArchive);
            ResponseMessage responseArchive = client.WaitForResponse();



            RequestMessage requestGetAttrList = OperationFactory.GetAttributeList(responseArchive.GetFirstReturnedId());
            client.SendRequest(requestGetAttrList);
            ResponseMessage responseGetAttrList = client.WaitForResponse();





            client.Close();
            Assert.AreEqual(ResultReasonType.ObjectArchived, responseGetAttrList.BatchItem.ResultReason);
            ////Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }





        [TestMethod]
        public void ArchiveFailed()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details,false);
            RequestMessage request = OperationFactory.Archive(recArchId);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.OperationFailed, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void Recover()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details,false);
            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

            request = OperationFactory.Archive(response.GetFirstReturnedId());
            client.SendRequest(request);
             response = client.WaitForResponse();

             request = OperationFactory.Recover(response.GetFirstReturnedId());
             client.SendRequest(request);
             response = client.WaitForResponse();
            
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }




        [TestMethod]
        public void DeriveKey()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

            request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 100, DerivationMethodType.PBKDF2, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23,32,23 }, IterationCount = 10 });
            client.SendRequest(request);
            response = client.WaitForResponse();

            request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
            client.SendRequest(request);
            response = client.WaitForResponse();

            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

         [TestMethod]
        public void DeriveKeyHASH_SHA1()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

            request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 100, DerivationMethodType.HASH, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23, 32, 23 }, IterationCount = 10, CryptoParameters = new CryptographicParameters() { HashingAlgorithm=HashingAlgorithmType.SHA_1} });
            client.SendRequest(request);
            response = client.WaitForResponse();

            request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
            client.SendRequest(request);
            response = client.WaitForResponse();

            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }


         [TestMethod]
         public void DeriveKeyHASH_Whirpool()
         {

             IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
             ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
             client.Connect(details, false);
             RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
             client.SendRequest(request);
             ResponseMessage response = client.WaitForResponse();

             request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 100, DerivationMethodType.HASH, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23, 32, 23 }, IterationCount = 10, CryptoParameters = new CryptographicParameters() { HashingAlgorithm = HashingAlgorithmType.Whirlpool } });
             client.SendRequest(request);
             response = client.WaitForResponse();

             request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
             client.SendRequest(request);
             response = client.WaitForResponse();

             client.Close();
             Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
             //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


         }

         [TestMethod]
         public void DeriveKeyHASH_Tiger()
         {

             IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
             ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
             client.Connect(details, false);
             RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
             client.SendRequest(request);
             ResponseMessage response = client.WaitForResponse();

             request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 100, DerivationMethodType.HASH, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23, 32, 23 }, IterationCount = 10, CryptoParameters = new CryptographicParameters() { HashingAlgorithm = HashingAlgorithmType.Tiger } });
             client.SendRequest(request);
             response = client.WaitForResponse();

             request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
             client.SendRequest(request);
             response = client.WaitForResponse();

             client.Close();
             Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
             //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


         }



         [TestMethod]
         public void Get_Digest()
         {

             IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
             ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
             client.Connect(details, false);
             RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
             client.SendRequest(request);
             ResponseMessage response = client.WaitForResponse();

        

             request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
             client.SendRequest(request);
             response = client.WaitForResponse();

             client.Close();
             Assert.AreEqual(32, ((SymmetricKey)response.BatchItem.ResponsePayload.Attribute.Items[Constants.CRYPTO_OBJECT]).DigestValue.Value.Length);
             //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


         }
        
        [TestMethod]
         public void DeriveKeyHASH_SHA256()
         {

             IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
             ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
             client.Connect(details, false);
             RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
             client.SendRequest(request);
             ResponseMessage response = client.WaitForResponse();

             request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 100, DerivationMethodType.HASH, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23, 32, 23 }, IterationCount = 10, CryptoParameters = new CryptographicParameters() { HashingAlgorithm = HashingAlgorithmType.SHA_256 } });
             client.SendRequest(request);
             response = client.WaitForResponse();

             request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
             client.SendRequest(request);
             response = client.WaitForResponse();

             client.Close();
             Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
             //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


         }
         [TestMethod]
         public void DeriveKeyHMAC_RIPED160()
         {

             IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
             ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
             client.Connect(details, false);
             RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
             client.SendRequest(request);
             ResponseMessage response = client.WaitForResponse();

             request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 100, DerivationMethodType.HMAC, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23, 32, 23 }, IterationCount = 10, CryptoParameters = new CryptographicParameters() { HashingAlgorithm = HashingAlgorithmType.RIPEMD_160 } });
             client.SendRequest(request);
             response = client.WaitForResponse();

             request = OperationFactory.GetAttributes(response.GetFirstReturnedId());
             client.SendRequest(request);
             response = client.WaitForResponse();

             client.Close();
             Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
             //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


         }

        [TestMethod]
        public void FDeriveKeyTooLongCryptoLength()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details, false);
            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

            request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 40001, DerivationMethodType.PBKDF2, new DerivationParameters() { Salt = new byte[] { 23, 56, 76, 54, 32, 23, 32, 23 }, IterationCount = 10 });
            client.SendRequest(request);
            response = client.WaitForResponse();


            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultReasonType.CryptographicFailure, response.BatchItem.ResultReason);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void RecoverFailed()
        {

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details,false);
            RequestMessage request = OperationFactory.Recover(recArchId);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.OperationFailed, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }
    }
}
