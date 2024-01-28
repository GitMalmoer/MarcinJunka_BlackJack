using Data_Access;
using Data_Acess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DAL_Tests;
public class TestHostFixture
{
    public IHost Host { get; }

        public TestHostFixture()
        {
            Host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Replace AppDbContext with the TestAppDbContext using the in-memory provider.
                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    },ServiceLifetime.Transient);
                    services.AddTransient<IAppDbContext, AppDbContext>();

                    services.AddTransient<IGameRepository,GameRepository>();

                    // Add your test services here.
                })
                .Build();

            Host.StartAsync().Wait();
        }
    }


