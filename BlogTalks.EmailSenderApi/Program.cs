using BlogTalks.EmailSenderAPI.Models;
using BlogTalks.EmailSenderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/email", (EmailDTO request, IEmailSender emailSender) =>
{
    emailSender.Send(request);
    return Results.Ok();
});

app.Run();
