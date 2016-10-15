using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSUnion.KMIP.API;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Tests
{
    [TestClass]
    public class OperationTests
    {
        Guid recArchId = new Guid("9DE54FFF-8830-4CA3-A287-DA2FACA6571F");
        Guid addAttr = new Guid("9DE54FFF-8830-4CA3-A287-DA2FACA6571F");


        [TestMethod]
        public void AddAttribute()
        {
            string attrName="PersonOrderKey";
            string attrValue=@"http:\\331\34\3";
            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.AddAttribute(addAttr,attrName,attrValue);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(attrValue, response.BatchItem.ResponsePayload.Attribute.Items[attrName]);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }


        [TestMethod]
        public void ModifyAttribute()
        {
            string attrName = "PersonOrderKey";
            string attrValue = @"http:\\331\34\3\5\7\5\4\4";
            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.ModifyAttribute(addAttr, attrName, attrValue);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(attrValue, response.BatchItem.ResponsePayload.Attribute.Items[attrName]);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void Archive() 
        {
          
            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.Archive(recArchId);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual( Core.Enumerators.ResultStatusType.Success,response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);

        
        }

        [TestMethod]
        public void ObtainLease()
        {
            Guid id = new Guid("0A20515E-829C-45E8-A48A-1813B7472155");
            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.ObtainLease(id);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.IsNotNull(response.BatchItem.ResponsePayload.Attribute.Items["LeaseTime"]);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }


        [TestMethod]
        public void DeleteAll()
        {

            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.DeleteAll();
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void CreateAES()
        {

            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.Create(Core.Enumerators.KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.PrivateKey);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success, response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void ArchiveFailed()
        {
            
            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.Archive(recArchId);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.OperationFailed,response.BatchItem.ResultStatus);
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void Recover()
        {
           
            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.Recover(recArchId);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.Success,response.BatchItem.ResultStatus );
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }

        [TestMethod]
        public void RecoverFailed()
        {

            ServerFabric client = ServerFabric.GetInstanse();
            client.Connect(false);
            RequestMessage request = OperationFabric.Recover(recArchId);
            client.SendRequest(request, Core.MessageHelperType.Simple);
            ResponseMessage response = client.WaitForResponse(Core.MessageHelperType.Simple);
            client.Close();
            Assert.AreEqual(Core.Enumerators.ResultStatusType.OperationFailed,response.BatchItem.ResultStatus );
            //Assert.AreEqual(response.BatchItem.ResponsePayload.UniqueIdentifier.First(),id);


        }
    }
}
