using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace RabbitMqClient;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly IRabbitMq _rabbitMq;

    public RabbitMqPublisher(ILogger<RabbitMqPublisher> logger, IRabbitMq rabbitMq)
    {
        _logger = logger;
        _rabbitMq = rabbitMq;
    }

    private void BasicPublish(IConnection connection, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = false)
    {
        if (exchange == null) throw new ArgumentNullException(nameof(exchange));
        if (routingKey == null) throw new ArgumentNullException(nameof(routingKey));
        
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue,
            durable,
            exclusive,
            autoDelete,
            arguments);

        var body = Encoding.UTF8.GetBytes(message);

        if (persistent)
        {
            basicProperties ??= channel.CreateBasicProperties();
            basicProperties.Persistent = true;
        }

        channel.BasicPublish(exchange,
            routingKey,
            basicProperties,
            body);
    }

    #region Basic Params
    
    public void BasicPublish(string queue, string message, bool persistent = false)
    {
        BasicPublish(queue, message, "", queue, persistent: persistent);
    }

    public void BasicPublish(string clientProvidedName, string queue, string message, bool persistent = false)
    {
        BasicPublish(clientProvidedName, queue, message, "", queue, persistent: persistent);
    }

    public void BasicPublish(IList<string> hostnames, string queue, string message, bool persistent = false)
    {
        BasicPublish(hostnames, queue, message, "", queue, persistent: persistent);
    }

    public void BasicPublish(IList<string> hostnames, string clientProvidedName, string queue, string message, bool persistent = false)
    {
        BasicPublish(hostnames, clientProvidedName, queue, message, "", queue, persistent: persistent);
    }

    public void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string queue, string message, bool persistent = false)
    {
        BasicPublish(endpoints, queue, message, "", queue, persistent: persistent);
    }

    public void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, string message, bool persistent = false)
    {
        BasicPublish(endpoints, clientProvidedName, queue, message, "", queue, persistent: persistent);
    }

    public void BasicPublish(IEndpointResolver endpointResolver, string clientProvidedName, string queue, string message, bool persistent = false)
    {
        BasicPublish(endpointResolver, clientProvidedName, queue, message, "", queue, persistent: persistent);
    }

    #endregion

    #region Full Params

    public void BasicPublish(string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection();
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicPublish(string clientProvidedName, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(clientProvidedName);
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }
    
    public void BasicPublish(IList<string> hostname, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(hostname);
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicPublish(IList<string> hostnames, string clientProvidedName, string queue, string message, string exchange,
        string routingKey, IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false,
        bool autoDelete = false, IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(hostnames, clientProvidedName);
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(endpoints);
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, string message, string exchange,
        string routingKey, IBasicProperties basicProperties = null, bool durable = false, bool exclusive = false,
        bool autoDelete = false, IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(endpoints, clientProvidedName);
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicPublish(IEndpointResolver endpointResolver, string clientProvidedName, string queue, string message,
        string exchange, string routingKey, IBasicProperties basicProperties = null, bool durable = false,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null, bool persistent = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(endpointResolver, clientProvidedName);
            BasicPublish(connection, queue, message, exchange, routingKey, basicProperties, durable, exclusive,
                autoDelete, arguments, persistent);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    #endregion
}