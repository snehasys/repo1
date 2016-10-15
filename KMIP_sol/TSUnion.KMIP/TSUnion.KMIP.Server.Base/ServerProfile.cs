using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using log4net;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Server.Base
{
    public delegate void TaskAddedEventHandler(Guid task,IPAddress ip );

    public class ServerProfile
    {
      

        private static ServerProfile instance;
        private static ILog Log = LogManager.GetLogger(typeof(ServerProfile));

        private Guid placeholder;

        public event TaskAddedEventHandler TaskAdded;
        public static ServerProfile Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerProfile();
                }
                return instance;
            }
        }

        private ServerProfile()
        {
            tasksQueue = new Queue<ServerTask>();
            Log.Info("Task Queue has been initialized...");
        }
        public ServerSpecification Specification
        {
            get
            {
                return ServerSpecification.GetConfigSpecification();
            }
        }

        public Guid Placeholder { get; set; }

        private Queue<ServerTask> tasksQueue;
        private object locker = new object();

        public void AddTask(ServerTask task)
        {
            lock (locker)
            {
                tasksQueue.Enqueue(task);
            }

            Log.Debug("[AddTask].Tasks in the queue: " + GetQueueSize());
            if (TaskAdded != null)
            {
                TaskAdded(task.ID, task.IP);
            }
        }

        public int GetQueueSize()
        {
            return tasksQueue.Count;
        }

        public ServerTask TakeTaskFromQueue()
        {
            ServerTask task = null;
            Log.Debug("[TakeTaskFromQueue].Tasks in the queue: " + GetQueueSize());

            if (tasksQueue != null && tasksQueue.Count >= 0)
            {
                lock (locker)
                {
                    task = tasksQueue.Dequeue();
                }
                Log.Debug("[TakeTaskFromQueue].Task retreived from the queue.");
                Log.Debug("[TakeTaskFromQueue].Tasks in the queue: " + GetQueueSize());
            }

            return task != null ? task : ServerTask.Default();
        }


    }
}
