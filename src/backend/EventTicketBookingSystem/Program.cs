using System.Text;
using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Events.Repositories;
using EventTicketSystem.Modules.Events.Services;
using EventTicketSystem.Modules.Notifications.Consumers;
using EventTicketSystem.Modules.Notifications.Services;
using EventTicketSystem.Modules.Orders.Repositories;
using EventTicketSystem.Modules.Orders.Services;
using EventTicketSystem.Modules.Payments.Repositories;
using EventTicketSystem.Modules.Payments.Services;
using EventTicketSystem.Modules.Tickets.Repositories;
using EventTicketSystem.Modules.Tickets.Services;
using EventTicketSystem.Modules.Users.Repositories;
using EventTicketSystem.Modules.Users.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EventTicketSystem.Modules.Locations.Services;
using EventTicketSystem.Modules.Locations.Repositories;

var builder = WebApplication.CreateBuilder(args);

// -- Database --
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// -- Users module --
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

// -- Events module --
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IEventsService, EventsService>();

// -- Tickets module --
builder.Services.AddScoped<ISeatsRepository, SeatsRepository>();
builder.Services.AddScoped<ISeatsService, SeatsService>();

// -- Orders module --
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

// -- Payments module --
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();

// моя зміна
builder.Services.AddScoped<ILocationsService, LocationsService>();
// Тимчасово реєструємо мок, щоб проєкт запустився
builder.Services.AddSingleton<ILocationsRepository, MockLocationsRepository>();

// -- Notifications module --
builder.Services.AddScoped<IEmailService, EmailService>();

// -- MassTransit (RabbitMQ) --
builder.Services.AddMassTransit(x =>
{
    // Реєструємо консюмерів тут
    x.AddConsumer<PaymentConfirmedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/");

        // Налаштовуємо отримання повідомлень
        cfg.ReceiveEndpoint("payment-confirmed-queue", e =>
        {
            e.ConfigureConsumer<PaymentConfirmedConsumer>(context);
        });
    });
});

// -- API & Auth --
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EventTicketSystem", Version = "v1" });
});

var app = builder.Build();

// -- Middleware --
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();