using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Threading;
using SampleApi.Models;

namespace SampleApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                await WaitForMigrations(host, context);
            }

            var task = host.RunAsync();

            Notify("� App Running!");

            WebHostExtensions.WaitForShutdown(host);
        }

        private static async Task<int> MigrationCount(DbContext context)
        {
            return (await context.Database.GetPendingMigrationsAsync()).Count();
        }

        private static async Task WaitForMigrations(IWebHost host, DbContext context)
        {
            if (await MigrationCount(context) == 0)
            {
                return;
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                do
                {
                    Console.WriteLine("Waiting for migrations to complete...");
                    Thread.Sleep(1000);
                } while (await MigrationCount(context) > 0);

                return;
            }

            Console.WriteLine("Starting to migrate database....");
            try
            {
                await context.Database.MigrateAsync();
                Console.WriteLine("Database is up to date, #party time");
            }
            catch (DbException)
            {
                Notify("Database Migration FAILED");
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void Notify(string message)
        {
            var isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var isMac = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            if (!isWindows && !isMac)
            {
                return;
            }

            // Create a process to launch the nodejs app `notifiy` with our message
            var process = isMac ? new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ".bin/terminal-notifier.app/Contents/MacOS/terminal-notifier",
                    Arguments = $"-message \"{message}\" -title \"SampleApi\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            } : new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ".bin/snoretoast",
                    Arguments = $"-silent -m \"{message}\" -t \"SampleApi\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            // Start the message but do not wait for it to end, we don't care about the termination result.
            process.Start();
        }
    }
}
