using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Send
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(" Type message to send.");
                SendMessage(Console.ReadLine());
            }
        }

        private static void SendMessage(string messageText)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq.trin.tech", UserName = "user", Password = "1Trintech2018" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = string.Format("Sent Message: '{0}'", messageText);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
