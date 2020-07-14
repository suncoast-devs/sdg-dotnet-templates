using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleApi.Models;
using SampleApi.Utils;

namespace SampleApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Utilities.CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                await Utilities.WaitForMigrations(host, context);
            }

            var task = host.RunAsync();

            Utilities.Notify("SampleApi Running!");

            WebHostExtensions.WaitForShutdown(host);
        }
    }
}
