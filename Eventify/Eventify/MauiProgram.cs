using System.Reflection;
using Eventify.Core.Interfaces;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Core.Interfaces.Services;
using Eventify.Core.Services;
using Eventify.Infrastructure.Data;
using Eventify.Infrastructure.Repositories;
using Eventify.Models.States;
using Eventify.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Eventify
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Eventify.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(config);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<EventifyDbContext>(options =>
                options.UseNpgsql(connectionString));

            //repositorios
            builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
            builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            builder.Services.AddScoped<IEventoRepository, EventoRepository>();
            builder.Services.AddScoped<IIngressoRepository, IngressoRepository>();
            builder.Services.AddScoped<ICategoriasIngressoRepository, CategoriasIngressoRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //servicos
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IEstadoService, EstadoService>();
            builder.Services.AddScoped<ICidadeService, CidadeService>();
            builder.Services.AddScoped<IEventoService, EventoService>();
            builder.Services.AddScoped<IIngressoService, IngressoService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ICategoriasIngressoService, CategoriasIngressoService>();


            //states
            builder.Services.AddSingleton<PedidoStateService>();

            //maui
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
{
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                // RODA TUDO EM UMA THREAD DE BACKGROUND, EVITANDO O DEADLOCK
                Task.Run(() => Data.DataSeeker.SeedAsync(unitOfWork)).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
