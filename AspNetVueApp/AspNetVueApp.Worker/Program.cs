using Hangfire;
using Hangfire.SqlServer;
using AspNetVueApp.Worker;
using AspNetVueApp.Infrastructure.Jobs; // Importar la carpeta donde estar�n los Jobs
using Microsoft.Extensions.DependencyInjection;
using AspNetVueApp.Infrastructure.Persistence;
using AspNetVueApp.Domain.Interfaces;
using AspNetVueApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // 1. Configuraci�n de Hangfire con SQL Server
        services.AddHangfire(config =>
            config.UseSqlServerStorage("Server=.;Database=AspNetVueAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));


        services.AddHangfireServer(); // Inicializa el servidor de Hangfire

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=.;Database=AspNetVueAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));

        // 2. Configuraci�n de los servicios de Jobs
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserJob>(); // Registrar el Job como un servicio

        // 3. Configuraci�n del Worker
        services.AddHostedService<Worker>();
    });

var host = builder.Build();

// 4. Configuraci�n de los Jobs de Hangfire
using (var scope = host.Services.CreateScope())
{
    var jobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

    // Programar un Job inmediato
    jobClient.Enqueue<UserJob>(job => job.ProcessUsersAsync());

    // Programar un Job recurrente (cada minuto como ejemplo)
    RecurringJob.AddOrUpdate<UserJob>(
        "ProcessUsersRecurring", // Nombre del Job
        job => job.ProcessUsersAsync(), // M�todo a ejecutar
        Cron.Hourly()); // Frecuencia (usando Cron de Hangfire)
}

host.Run();
