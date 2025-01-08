using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.Services;

public class CommunicationService(IBus bus, ILogger<CommunicationService> logger) : ICommunicationService
{
    public async Task SendEmailAsync(SendEmail sendEmailDto)
    {
        logger.LogInformation("Sending message to {recipient}", sendEmailDto.ReceiverEmail);
        await bus.Publish(sendEmailDto);
    }
}