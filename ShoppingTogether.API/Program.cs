using Azure.Identity;
using ShoppingTogether.API.Extensions;
using ShoppingTogether.API.Users;
using ShoppingTogether.API.Users.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices(builder.Configuration);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["KeyVault:Uri"]!),
    new DefaultAzureCredential());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", async (IUserRepository userRepository) =>
{
    return await userRepository.GetAllUsersAsync();
});

app.MapGet("/users/{sid}", async (string sid, IUserRepository userRepository) =>
{
    return await userRepository.GetUserBySidAsync(sid);
});

app.MapPost("/users", async (User user, IUserRepository userRepository) =>
{
    return await userRepository.AddUserAsync(user.Name, user.Sid);
});

app.Run();