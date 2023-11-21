using Conferences.Api.DAL;

namespace Conferences.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();

            await SeedDatabaseAsync(host);

            await host.RunAsync();

            await Task.CompletedTask;
        }

        private static async Task SeedDatabaseAsync(IHost host)
        {
            using (var scope = host.Services.CreateAsyncScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var conferenceContext = serviceProvider.GetRequiredService<ConferenceContext>();
                await ConferenceContextSeed.SeedAsync(conferenceContext);
            }
        }

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
        {    
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}