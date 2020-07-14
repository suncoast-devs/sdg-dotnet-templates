using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleSite.Models;
using SampleSite.Utils;

namespace SampleSite
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

            Utilities.Notify("SampleSite Running!"); ;

            WebHostExtensions.WaitForShutdown(host);
        }
    }
}
