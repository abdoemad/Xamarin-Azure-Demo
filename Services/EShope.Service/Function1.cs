using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace EShope.Service
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("notificationqueue", Connection = "eshopestorage")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
