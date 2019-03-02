using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace EShope.Service
{
    public static class NotifierFunction
    {
        [FunctionName("NotifierFunction")]
        public static void Run([QueueTrigger("notificationqueue", Connection = "conn")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
