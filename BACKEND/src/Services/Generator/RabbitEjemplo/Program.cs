//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System;
//using System.Text;

//namespace RabbitEjemplo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
            
//                var factory = new ConnectionFactory() { HostName = "localhost" };
//                //creamos una conexion
//                using (var connection = factory.CreateConnection())
//                {
//                    // creamos un canal
//                    using (var channel = connection.CreateModel())
//                    {
//                        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

//                        var consumer = new EventingBasicConsumer(channel);

//                        consumer.Received += (model, ea) =>
//                        {
//                            var body = ea.Body.ToArray();
//                            var message = Encoding.UTF8.GetString(body);

//                            Console.WriteLine($"[x] Received {message}");
                            
//                        };

//                        channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
//                    }
//                    //Console.WriteLine("press any key to exit...");
//                    //Console.ReadLine();
//                }

            
//        }
//    }
//}
