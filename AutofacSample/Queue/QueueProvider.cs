using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using RestEaseSample;

namespace AutofacSample.Queue
{
    public interface IQueueProvider<T>
    {
        public AmazonSQSClient GetQueueClient();

        public SendMessageResponse SendMessage(T user);

    }
    public class QueueProvider<T> : IQueueProvider<T>
    {
        public static string AwsAccess = "";
        public static string AwsSecret = "";
        private readonly AmazonSQSClient _amazonSqsClient;
        public const string QueueUrl = "https://sqs.eu-west-2.amazonaws.com/896009684607/sample_imagine";

        public QueueProvider()
        {
            var awsCreds = new BasicAWSCredentials(AwsAccess,
                AwsSecret);
            _amazonSqsClient = new AmazonSQSClient(awsCreds, Amazon.RegionEndpoint.EUWest2);
        }

        public AmazonSQSClient GetQueueClient()
        {
            return _amazonSqsClient;
        }

        public SendMessageResponse SendMessage(T user)
        {
            var sendRequest = new SendMessageRequest();
            sendRequest.QueueUrl = QueueUrl;
            sendRequest.MessageBody = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            return _amazonSqsClient.SendMessageAsync(sendRequest).Result;

        }
    }

    public class MockQueueProvider<T> : IQueueProvider<T>
    {
        public AmazonSQSClient GetQueueClient()
        {
            throw new System.NotImplementedException();
        }

        public SendMessageResponse SendMessage(T user)
        {
            throw new System.NotImplementedException();
        }
    }
}