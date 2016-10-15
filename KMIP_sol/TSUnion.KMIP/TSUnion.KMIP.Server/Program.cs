using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;
using log4net.Config;
using TSUnion.KMIP.Communication;
using TSUnion.KMIP.Communication.Transport;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;
using TSUnion.KMIP.DAO;
using TSUnion.KMIP.Operations;
using TSUnion.KMIP.Server.Base;

namespace TSUnion.KMIP.Server
{
    public class Server
    {




     

        private static ILog Log = LogManager.GetLogger(typeof(Server));
        private static IServerTransport serverChannel = null;

        static void Main(string[] args)
        {
            Log.Info("");
            Log.Info("######################################################");
            Log.Info("#                                                    #");
            Log.Info("#                      KMIP Server                   #");
            Log.Info("#                                                    #");
            Log.Info("######################################################");
            Log.Info("Staring KMIP server...");

            XmlConfigurator.Configure();
          
            try
            {

                ConnectionDetails details = ConnectionDetails.CreateConnectionFromConfig();
                Log.Info("Checking whether the connection to the DB is available...");

                //if (!DBHelper.IsServerConnected())
                //{
                //    Log.Error("Connection to the DB  is not available...");

                //    Environment.Exit(1);

                //}

                //Log.Info("Connection successful");

                serverChannel = TransportFactory.GetServerChannel(details.Transport);
                serverChannel.CreateServerTunnel(details);
                Log.Info("KMIP server started.");
                Log.Info("For more information please refer to http://docs.oasis-open.org/kmip/spec/v1.0/os/kmip-spec-1.0-os.pdf");



                ServerProfile.Instance.TaskAdded += delegate(Guid id, IPAddress adress)
                {
                    Log.Info("Server notified Execution Thread that new task arrived: " + id);
                    ServerTask task = ServerProfile.Instance.TakeTaskFromQueue();
                    Log.Info("Task retrieved from the Queue: " + task.ID);

                    OperationManager operationManger = new OperationManager();
                    ResponseMessage responseMessage = operationManger.Proceed(task.Request);
                    Log.Info("Response message generated.");
                    if (responseMessage.BatchItem.ResultStatus == ResultStatusType.Success)
                    {
                        ServerProfile.Instance.Placeholder = responseMessage.BatchItem.ResponsePayload.UniqueIdentifier.First();
                        Log.Info("Id Placeholder set up: " + ServerProfile.Instance.Placeholder);
                    }




                    serverChannel.SendResponseToClient(responseMessage, adress);

                };



                serverChannel.ServerRequestReceived += delegate(RequestMessage requestMessage, IPAddress sourceAddress)
                {
                    if (requestMessage == null)
                    {
                        serverChannel.SendResponseToClient((new OperationManager()).GenerateNotParsedErrorResponse(), sourceAddress);
                    }

                    //requestMessage.Push();
                    ServerProfile.Instance.AddTask(new ServerTask(requestMessage, sourceAddress));



                };

                Console.ReadKey();


            }
            catch (SocketException e)
            {
                Log.Info("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                if (serverChannel != null)
                    serverChannel.RemoveServerTunnel();
                Console.ReadKey();
            }


            Log.Info("\nHit enter to continue...");
            Console.Read();
        }
    }
}

