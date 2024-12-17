using Hangfire;
using Hangfire.SqlServer;
using AspNetVueApp.Worker;
using AspNetVueApp.Infrastructure.Jobs; // Importar la carpeta donde estarán los Jobs
using Microsoft.Extensions.DependencyInjection;
using AspNetVueApp.Infrastructure.Persistence;
using AspNetVueApp.Domain.Interfaces;
using AspNetVueApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // 1. Configuración de Hangfire con SQL Server
        services.AddHangfire(config =>
            config.UseSqlServerStorage("Server=.;Database=AspNetVueAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));


        services.AddHangfireServer(); // Inicializa el servidor de Hangfire

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=.;Database=AspNetVueAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));

        // 2. Configuración de los servicios de Jobs
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserJob>(); // Registrar el Job como un servicio

        services.AddScoped<ICatFactRepository, CatFactRepository>(); // Repositorio
        services.AddScoped<CatFactJob>();

        // 3. Configuración del Worker
        services.AddHostedService<Worker>();
    });

var host = builder.Build();

// 4. Configuración de los Jobs de Hangfire
using (var scope = host.Services.CreateScope())
{
    var jobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

    // Programar un Job inmediato
    jobClient.Enqueue<UserJob>(job => job.ProcessUsersAsync());
    jobClient.Enqueue<CatFactJob>(job => job.ProcessCatFactsAsync());

    RecurringJob.AddOrUpdate<UserJob>(
        "ProcessUsersRecurring", 
        job => job.ProcessUsersAsync(), 
        Cron.Hourly());

    RecurringJob.AddOrUpdate<CatFactJob>(
        "ProcessCatFactsRecurring",
        job => job.ProcessCatFactsAsync(),
        Cron.Hourly());

}

host.Run();
