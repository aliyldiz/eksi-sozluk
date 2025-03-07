namespace EksiSozluk.Projections.UserService.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateConfirmationLink(Guid confirmationId)
    {
        var baseUrl = _configuration["ConfirmationLink"] + confirmationId;
        return baseUrl;
    }

    public Task SendEmail(string toEmailAddress, string content)
    {
        _logger.LogInformation($"Email sent to {toEmailAddress} with content: {content}");
        return Task.CompletedTask;
    }
}