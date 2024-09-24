using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.Messaging
{
    public class RabbitMQProducer
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        // Constructor que inicializa la conexión a RabbitMQ
        public RabbitMQProducer(IConfiguration configuration)
        {
            _hostname = configuration["RabbitMQ:Host"];
            _port = int.Parse(configuration["RabbitMQ:Port"]);
            _username = configuration["RabbitMQ:UserName"];
            _password = configuration["RabbitMQ:Password"];
            _queueName = "orders_queue";

            // Configuración del cliente para conectarse a RabbitMQ
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                Port = _port,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declaración de la cola en RabbitMQ
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        // Método para publicar un mensaje en la cola de RabbitMQ
        public void Publish<T>(T message)
        {
            var jsonMessage = JsonSerializer.Serialize(message); // Serializa el mensaje a JSON
            var body = Encoding.UTF8.GetBytes(jsonMessage); // Convierte el mensaje a bytes

            // Publica el mensaje en la cola
            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }

        // Cierra la conexión y el canal cuando se finaliza el uso
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

}
