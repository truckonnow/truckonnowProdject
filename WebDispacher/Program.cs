using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebDispacher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args);
            BuildWebHost(args).Run(); 
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}