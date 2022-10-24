using System.Text.Json;
using Bogus;
using Bogus.Extensions.Brazil;
using RabbitMqClient;

namespace RabbitMqPublisher;

public class TesteRabbitMq : ITesteRabbitMq
{
    private readonly IRabbitMqPublisher _rabbitMqPublisher;

    public Faker Faker { get; private set; }

    public TesteRabbitMq(IRabbitMqPublisher rabbitMqPublisher)
    {
        _rabbitMqPublisher = rabbitMqPublisher;
    }

    public async Task Run()
    {
        var queueName = "TestQueue";

        while (true)
        {
            Faker = new Faker("pt_BR");
            var message = JsonSerializer.Serialize(new
            {
                TranceIdentifier = Guid.NewGuid(),
                Data = new
                {
                    Name = Faker.Person.FullName,
                    Document = Faker.Person.Cpf()
                }
            });

            _rabbitMqPublisher.BasicPublish(queueName, message, "", queueName, persistent: true);

            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Message SENT at: {DateTime.Now.ToString("G")} \nMessage: {message}");
            Console.ForegroundColor = foregroundColor;

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}