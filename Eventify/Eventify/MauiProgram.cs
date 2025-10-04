using Eventify.Core.Interfaces.Repositories;
using Eventify.Core.Interfaces.Services;
using Eventify.Core.Services;
using Eventify.Infrastructure.Data;
using Eventify.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

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


            //maui
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
