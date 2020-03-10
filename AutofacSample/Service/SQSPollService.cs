using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace AutofacSample.Service
{
    public class SQSPollService : BackgroundService
    {
        private readonly ILogger<SQSPollService> _logger;
        public static string aws_access = "";
        public static string aws_secret = "";


        public SQSPollService(ILogger<SQSPollService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"GracePeriodManagerService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" GracePeriod background task is stopping."));
            var queueUrl = "https://sqs.eu-west-2.amazonaws.com/896009684607/sample_imagine";

            var receiveMessageRequest = new ReceiveMessageRequest();
            receiveMessageRequest.QueueUrl = queueUrl;
            var awsCreds = new BasicAWSCredentials(aws_access,
                aws_secret);

            var amazonSQSClient = new AmazonSQSClient(awsCreds, Amazon.RegionEndpoint.EUWest2);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"GracePeriod task doing background work.");
                Console.WriteLine("Receiving Message");




                var response = amazonSQSClient.ReceiveMessageAsync(receiveMessageRequest).Result;

                if (response.Messages.Any())
                {
                    foreach (var message in response.Messages)
                    {
                        //Spit it out
                        Console.WriteLine(message.Body);

                        //Remove it from the queue as we don't want to see it again
                        var deleteMessageRequest = new DeleteMessageRequest();
                        deleteMessageRequest.QueueUrl = queueUrl;
                        deleteMessageRequest.ReceiptHandle = message.ReceiptHandle;

                        var result = amazonSQSClient.DeleteMessageAsync(deleteMessageRequest).Result;
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogDebug($"GracePeriod background task is stopping.");
        }

        
    }
}