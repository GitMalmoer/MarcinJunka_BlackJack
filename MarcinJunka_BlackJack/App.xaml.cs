using System.Windows;
using Business_Logic;
using Data_Access;
using Data_Acess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarcinJunka_BlackJack;


public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnectionString")),
                    ServiceLifetime.Scoped
                    );
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);
            services.AddScoped<IGameRepository, GameRepository>();




            services.AddSingleton<GameMechanics>(provider =>
            {
                GameMechanics gameMechanics = new GameMechanics(provider.GetService<IGameRepository>()!);
                return gameMechanics;
            });

            // APPSETTINGS.JSON FILE NEEDED TO BE RE-CONFIGURED. FILEINFO(proporties) > copy_to_output directory = Copy always
            var cstring = context.Configuration.GetSection("ConnectionStrings:DefaultConnectionString");
        }).Build();
    }


    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
        startupWindow.Show();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppHost!.StopAsync();
        base.OnExit(e);
    }
}



