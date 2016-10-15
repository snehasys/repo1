using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using log4net.Config;
using TSUnion.KMIP.API;
using TSUnion.KMIP.Communication;
using TSUnion.KMIP.Communication.Transport;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.KeyMaterials;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

using TSUnion.KMIP.TestClient.ManagedDLL;

namespace TSUnion.KMIP.TestClient
{
    class Program
    {
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        static void Main(string[] args)
        {
           

            XmlConfigurator.Configure();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Thread.Sleep(500);

            Exposed_Interface c1 = new ManagedDLL.COMinterop_Exposed_Class();
            Console.WriteLine(c1.getSampleMsg(41));
            String uid = c1.register_ops("256bit sample paswrd givenby SCS", "user1", "india@123");
            
            Console.ReadKey();


            //ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            //IClientTransport client = TransportFactory.CreateClientChannel(details.Transport);

            //client.Connect(details);


            //RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.DES3, new CryptographicUsageMask() { DeriveKey = true }, ObjectType.SymmetricKey);
            //client.SendRequest(request);
            //ResponseMessage responseCreate = client.WaitForResponse();

            //Guid keyId = responseCreate.GetFirstReturnedId();


            //RequestMessage requestGet = OperationFactory.Get(keyId);
            //client.SendRequest(requestGet);
            //ResponseMessage responseGet = client.WaitForResponse();

            //BaseObject obj = null;
            //if (responseGet.IsIncludeManagedObject())
            //{
            //    obj = responseGet.GetIncludedManagedObject();
            //}


            //KeyBlock<SymmetricKeyMaterial> block = KeyMaterialConverter<KeyBlock<SymmetricKeyMaterial>>.ConvertFrom(obj.Key);


            //string cryptoText = Crypto.EncryptStringAES("hello", block.Value.Instance.Key, block.Salt, block.Iterations);
            //string normalText = Crypto.DecryptStringAES(cryptoText, block.Value.Instance.Key, block.Salt, block.Iterations);
            //Console.WriteLine("Decoded value: " + normalText);
            //client.Close();

            string attrName =  "PersonOrderKey";
            string attrValue = @"http:\\331\34\3\5\7\5\4\4";
            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            client.Connect(details);

            RequestMessage request = OperationFactory.Create(KeyCryptographicAlgorithmType.AES, new Core.CryptographicUsageMask() { DeriveKey = true }, Core.Enumerators.ObjectType.SymmetricKey);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();

            request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), "User_Name","NECusr1");
            client.SendRequest(request);
            response = client.WaitForResponse();

            //request = OperationFactory.ModifyAttribute(response.GetFirstReturnedId(), attrName, attrValue);
            //client.SendRequest(request);
            //response = client.WaitForResponse();
            //Dictionary<string,object> parameters= new Dictionary<string,object>();
            //parameters.Add("KeyCryptographicAlgorithm", KeyCryptographicAlgorithmType.AES);
            ////parameters.Add("Key", "THIS_IS_A_MANUALLY_GIVEN_KEY");
            
            //request = OperationFactory.Register(ObjectType.SymmetricKey, parameters);
            //client.SendRequest(request);
            //response = client.WaitForResponse();

            //Guid uuid = new Guid("{263bfbf9-29db-4bc1-ad2a-442ffdb3d89d}");

            DerivationParameters dParam = new DerivationParameters();
            dParam.CryptoParameters = new CryptographicParameters();
            dParam.CryptoParameters.HashingAlgorithm = HashingAlgorithmType.MD5;            
            dParam.InitializationVector = GetBytes("Sample_Initial_Vector");

            request = OperationFactory.Derive(response.GetFirstReturnedId(), KeyCryptographicAlgorithmType.AES, 192, DerivationMethodType.HASH, dParam);
            client.SendRequest(request);
            response = client.WaitForResponse();

            //Dictionary<string, object> d=new Dictionary<string,object>;
            //d.Add( "KeyCryptographicAlgorithm",KeyCryptographicAlgorithmType.AES );
            //request = OperationFactory.Register(ObjectType.SymmetricKey,
            //            ((TSUnion.KMIP.Core.Attributes.ManagedAttribute)response.BatchItem.ResponsePayload.Attribute).Items);
            //client.SendRequest(request);
            //response = client.WaitForResponse();
            
                
            


            //request = OperationFactory.Archive(response.GetFirstReturnedId());
            //client.SendRequest(request);
            //response = client.WaitForResponse();

            Guid uuid = new Guid("{1A9968EB-F481-452C-B1FF-75B34CA9EAF8}");
            request = OperationFactory.Get(uuid, KeyFormatType.TransparentSymmetricKey, KeyCompressionType.Extensions);
            client.SendRequest(request);
            response = client.WaitForResponse();


            BaseObject obj = response.GetIncludedManagedObject();            
            KeyBlock<SymmetricKeyMaterial> block = KeyMaterialConverter<KeyBlock<SymmetricKeyMaterial>>.ConvertFrom(obj.Key);
            Byte[] SymmetricKey = block.Value.Instance.Key;
            //Byte[] SymmetricKey = (((KeyBlock<SymmetricKeyMaterial>)
            //Dictionary<string,object> d=((TSUnion.KMIP.Core.Attributes.ManagedAttribute)response.BatchItem.ResponsePayload.Attribute).Items;

            string SymmetricKeyHex = BitConverter.ToString(SymmetricKey).Replace("-", ""); // converted byte to HexString
            string uuidAndKey = response.GetFirstReturnedId().ToString() + "\t" + SymmetricKeyHex + "\n";
            System.IO.File.AppendAllText(@"..\..\uid&key.txt", uuidAndKey, System.Text.Encoding.Unicode); // File appender

 
            client.Close();
            
            Console.WriteLine("\nUUID			                symmetric-KEY"+
                                "\n-------------------------------------	--------------------\n"
                                + uuidAndKey);
            Console.ReadKey();//halts the screen before quitting

        }
    }
}
