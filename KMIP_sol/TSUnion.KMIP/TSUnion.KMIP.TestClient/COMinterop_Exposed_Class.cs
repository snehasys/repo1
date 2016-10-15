using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
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

namespace TSUnion.KMIP.TestClient.ManagedDLL
{
    // Interface declaration
    public interface Exposed_Interface
    {
        /// <summary>
        /// register a new managed object with predefined keys
        /// </summary>
        /// <param name="K_all"> SCS provided key: Concatenated values of all the following keys (K_{Nb},K_{LB},K_{vCTR}, LBInit computes PCMAC_key[16], PCMAC_L[16], SumExp_key[16], SumExpCBC_key[16]; SumExpE_ekey[4*(14+1)],SumExpD_ekey[4*(14+1)], SumExpCBC_ekey[4*(14+1)], ekey, L[16],D[16],Q[16],Ld[16]) </param>
        /// <param name="new_username"></param>
        /// <param name="new_password"></param>
        /// <returns> The new uid of the registered object </returns>
        String register_ops(String K_all, String new_username, String new_password);


        /// <summary>
        /// creates new user by saving usr&passwd as extra attributes, returns new uuid
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="new_username"></param>
        /// <param name="new_password"></param>
        /// <param name="new_uuid"></param>
        /// <returns></returns>

        String create_ops(String algo, String new_username, String new_password); 
        
        /// <summary>
        /// returns new uuid
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="algo"></param>
        /// <param name="keyLength"></param>
        /// <param name="user_name"></param>
        /// <param name="passwd"></param>
        /// <param name="new_uuid"></param>
        /// <returns></returns>
 
        bool derive_ops(String uuid, String algo, UInt16 keyLength, String user_name, String passwd,  String new_uuid); 
                
        /// <summary>
        /// returns the key, if username&pwd is correct with respect to given uuid.    
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="user_name"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        String get_ops(String uuid, String user_name, String passwd); 


        //test string
        String getSampleMsg(int a);

    }

    // Interface implementation
    public class COMinterop_Exposed_Class : Exposed_Interface
    {
        private static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TransportFactory));           
         


        //Default Constructor
        public COMinterop_Exposed_Class(){
            XmlConfigurator.Configure(new System.IO.FileInfo(@"vitalApp.config"));
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Thread.Sleep(500);
            
        }

        private KeyCryptographicAlgorithmType getAlgoType (String algo){
            var algoType = new KeyCryptographicAlgorithmType();
            switch(algo.ToUpper()){                  //only supported symmetric algorithms scenario are considered here.
                case "AES":    algoType= KeyCryptographicAlgorithmType.AES;    break;
                case "DES":    algoType= KeyCryptographicAlgorithmType.DES;    break;
                case "DES3":   algoType= KeyCryptographicAlgorithmType.DES3;   break; 
                default:
                    throw new Exception("**Wrong algo given\n");
            }
            return algoType;
        }
        public String getSampleMsg(int a)
        {
            return "\n**this is working   "+a.ToString()+"\n**This message is from Exposed C# module***\n";
        }



        public String register_ops(String K_all, String new_username, String new_password){
            try{
                String new_uuid;                
                IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
                ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
                client.Connect(details);
                Log.Info("Good news: KLMS Server is alive...");

                CredentialStructure credentials = new CredentialStructure();
                credentials.Type=CredentialType.UsernameAndPassword;
                credentials.Value=new CredentialValue();
                credentials.Value.Username="user1";
                credentials.Value.Password = "india@123";
                

                KeyBlock<SymmetricKeyMaterial> newKey = new KeyBlock<SymmetricKeyMaterial>();
                newKey.Format = KeyFormatType.Raw;
                newKey.Compression = KeyCompressionType.ECPublicUncompressed;
                newKey.Value = new SymmetricKeyMaterial();
                ((SymmetricKeyMaterial)newKey.Value).Key = Encoding.UTF8.GetBytes(K_all);
                newKey.Length=K_all.Length;
                newKey.Algorithm=KeyCryptographicAlgorithmType.Extensions;

                Dictionary<string, object> dictionary=new Dictionary<string,object>();
                dictionary.Add("KeyCryptographicAlgorithm", newKey.Algorithm);
                dictionary.Add("KeyBlock", newKey);
                

                RequestMessage request = OperationFactory.Register(ObjectType.SymmetricKey, dictionary);
                request.Header.Authentication = credentials;
                client.SendRequest(request);
                ResponseMessage response = client.WaitForResponse();

                String attrName = "User_Name";
                String attrValue = new_username;
                request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), attrName, attrValue); // Adding new attribute User_Name
                client.SendRequest(request);
                response = client.WaitForResponse();

                attrName = "Password";
                attrValue = new_password;
                request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), attrName, attrValue);    // Adding new attribute Password
                client.SendRequest(request);
                response = client.WaitForResponse();
                                
                new_uuid = response.GetFirstReturnedId().ToString();// return the newly generated uuid


                request = OperationFactory.GetAttributes(new Guid(new_uuid),"User_Name");
                request.Header.Authentication = credentials;
                client.SendRequest(request);
                response = client.WaitForResponse();

                request = OperationFactory.ModifyAttribute(response.GetFirstReturnedId(), "User_Name", "NEC_UserModified");
                client.SendRequest(request);
                response = client.WaitForResponse();
                new_uuid = response.GetFirstReturnedId().ToString();// return the newly generated uuid







                client.Close();
                return new_uuid;
            }
            catch (Exception e){
                Log.Error(e.ToString());
                return "Exception Occured. See log for more details";
            }
        }

        /// <summary>
        /// Creates new user by saving usr&passwd as extra attributes, returns new uuid
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="new_username"></param>
        /// <param name="new_password"></param>
        /// <param name="new_uuid"></param>
        /// <returns></returns>
        /// 
        public String create_ops(String algo, String new_username, String new_password)
        {
            try {                 
                String new_uuid;
                IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
                ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();                
                client.Connect(details);
                Log.Info("Good news: KLMS Server is alive...");

                KeyBlock<SymmetricKeyMaterial> newKey= new KeyBlock<SymmetricKeyMaterial>() ;
                newKey.Format= KeyFormatType.Raw;
                newKey.Compression=KeyCompressionType.ECPublicUncompressed;
                newKey.Value = new SymmetricKeyMaterial();
                newKey.Value.Instance.Key = Encoding.UTF8.GetBytes("256bit sample secret givenby SCS");


                RequestMessage request = OperationFactory.Create(getAlgoType(algo),
                                            new Core.CryptographicUsageMask() { DeriveKey = true },
                                                Core.Enumerators.ObjectType.SymmetricKey); //only supported symmetric algorithms scenario are considered here.
                client.SendRequest(request);
                ResponseMessage response = client.WaitForResponse();

                String attrName = "User_Name";
                String attrValue = new_username;
                request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), attrName, attrValue); // Adding new attribute User_Name
                client.SendRequest(request);
                response = client.WaitForResponse();

                attrName = "Password";
                attrValue = new_password;
                request = OperationFactory.AddAttribute(response.GetFirstReturnedId(), attrName, attrValue);    // Adding new attribute Password
                client.SendRequest(request);
                response = client.WaitForResponse();

                new_uuid = response.GetFirstReturnedId().ToString();// return the newly generated uuid
                client.Close();
                return new_uuid;                
            }
            catch (Exception e){
                Log.Error(e.ToString());
                return "Exception Occured. See log for more details";
            }
            
        }

    /// <summary>
    /// //////////
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="algo"></param>
    /// <param name="keyLength"></param>
    /// <param name="user_name"></param>
    /// <param name="passwd"></param>
    /// <param name="new_uuid"></param>
    /// <returns></returns>
        public bool derive_ops(String uuid, String algo, UInt16 keyLength, String user_name, String passwd,  String new_uuid){

            IClientTransport client = TransportFactory.CreateClientChannel(TrasportType.TCP);
            ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
            
            DerivationParameters dParam = new DerivationParameters();
            dParam.CryptoParameters = new CryptographicParameters();
            dParam.CryptoParameters.HashingAlgorithm = HashingAlgorithmType.MD5;
            dParam.InitializationVector = new byte[] { 76, 33, 18, 57, 24, 74, 96, 85 };// Sample_Initial_Vector

            RequestMessage request = OperationFactory.Derive(new Guid(uuid), getAlgoType(algo), keyLength,
                                                                DerivationMethodType.HASH, dParam);
            client.SendRequest(request);
            ResponseMessage response = client.WaitForResponse();



            return true; 
        }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="user_name"></param>
    /// <param name="passwd"></param>
    /// <param name="yourKey"></param>
    /// <returns></returns>
        public String get_ops(String uuid, String user_name, String passwd){

            String yourKey = "howdy??";
            return yourKey;
        }

    }
}
