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

// -- Locations module --
builder.Services.AddScoped<ILocationsService, LocationsService>();
builder.Services.AddScoped<ILocationsRepository, LocationsRepository>(); // Змінили на Scoped і реальний клас

// -- Notifications module --
builder.Services.AddScoped<IEmailService, EmailService>();

// -- MassTransit (RabbitMQ) --
builder.Services.AddMassTransit(x =>
{

    // Реєструємо консюмерів тут
    x.AddConsumer<PaymentConfirmedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
{
    // 1. Отримуємо хост із конфігурації (у Docker це буде "dotnet_rabbitmq", локально — "localhost")
    var rabbitHost = builder.Configuration["RabbitMq:Host"] ?? "localhost";

    cfg.Host(rabbitHost, "/", h =>
    {
        // 2. Беремо кредеснціали із змінних оточення (як зробив Богдан)
        h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest");
        h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest");
    });

    // Налаштовуємо отримання повідомлень (це лишаємо як було)
    cfg.ReceiveEndpoint("payment-confirmed-queue", e =>
    {
        e.ConfigureConsumer<PaymentConfirmedConsumer>(context);
    });
});
});

// -- API & Auth --
builder.Services.AddControllers();

// -- CORS for local development --
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

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

    // Додаємо визначення безпеки (кнопку Authorize)
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Введіть JWT токен у форматі: Bearer {твій_токен}"
    });

    // Застосовуємо це правило до всіх ендпоінтів
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EventTicketSystem.Infrastructure.Data.AppDbContext>();
    // Ця команда автоматично створює всі таблиці в базі при запуску
    dbContext.Database.Migrate();
}
//
// -- Middleware --
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DevCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// -- Автоматичне застосування міграцій при старті --
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
