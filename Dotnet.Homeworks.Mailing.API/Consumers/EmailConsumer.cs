using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.Consumers;

public class EmailConsumer(IMailingService service, ILogger<EmailConsumer> logger) : IEmailConsumer
{
    public async Task Consume(ConsumeContext<SendEmail> context)
    {
        var message = new EmailMessage(
            context.Message.ReceiverEmail, 
            context.Message.Subject, 
            context.Message.Content);
        
        logger.LogInformation("Receiving message to {recipient}", message.Email);
        await service.SendEmailAsync(message);
    }
}