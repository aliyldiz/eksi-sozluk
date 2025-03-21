using EksiSozluk.Projections.UserService;
using EksiSozluk.Projections.UserService.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<EmailService>();

var host = builder.Build();
host.Run();