using EksiSozluk.Common;
using EksiSozluk.Common.Events.User;
using EksiSozluk.Common.Infrastructure;
using EksiSozluk.Projections.UserService.Services;

namespace EksiSozluk.Projections.UserService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly UserService.Services.UserService userService;
    private readonly EmailService emailService;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.UserExchangeName)
            .EnsureQueue(SozlukConstants.UserEmailChangedQueueName, SozlukConstants.UserExchangeName)
            .Receive<UserEmailChangedEvent>(user =>
            {
                // DB Insert 
                var confirmationId = userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();

                // Generate Link
                var link = emailService.GenerateConfirmationLink(confirmationId);

                // Send Email
                emailService.SendEmail(user.NewEmailAddress, link).GetAwaiter().GetResult();
            })
            .StartConsuming(SozlukConstants.UserEmailChangedQueueName);
    }
}