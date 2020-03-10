using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using AutofacSample.Queue;
using Microsoft.Extensions.Logging;
using RestEaseSample;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace AutofacSample.Service
{
    public class SQSPollService : BackgroundService
    {
        private readonly ILogger<SQSPollService> _logger;
        private readonly IQueueProvider<User> _queueProvider;



        public SQSPollService(ILogger<SQSPollService> logger, IQueueProvider<User> queueProvider)
        {
            _logger = logger;
            _queueProvider = queueProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"GracePeriodManagerService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" GracePeriod background task is stopping."));
           
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"GracePeriod task doing background work.");
                Console.WriteLine("Receiving Message");
                var receiveMessageRequest = new ReceiveMessageRequest();
                receiveMessageRequest.QueueUrl = QueueProvider<User>.QueueUrl;
                var response = _queueProvider.GetQueueClient().ReceiveMessageAsync(receiveMessageRequest).Result;

                if (response.Messages.Any())
                {
                    foreach (var message in response.Messages)
                    {
                        //Spit it out
                        Console.WriteLine(message.Body);

                        //Remove it from the queue as we don't want to see it again
                        var deleteMessageRequest = new DeleteMessageRequest();
                        deleteMessageRequest.QueueUrl = QueueProvider<User>.QueueUrl;
                        deleteMessageRequest.ReceiptHandle = message.ReceiptHandle;

                        var result = _queueProvider.GetQueueClient().DeleteMessageAsync(deleteMessageRequest).Result;
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogDebug($"GracePeriod background task is stopping.");
        }

        
    }
}