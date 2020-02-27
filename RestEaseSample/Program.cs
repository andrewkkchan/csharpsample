using System;
using Autofac;
using RestEase;

namespace RestEaseSample
{


    public class Program
    {
        private static IContainer Container { get; set; }

        public static void Main(string[] args)
        {
            // Create an implementation of that interface
            // We'll pass in the base URL for the API
            IGitHubContract api = RestClient.For<IGitHubContract>("https://api.github.com");

            // Now we can simply call methods on it
            // Normally you'd await the request, but this is a console app
            User user = api.GetUserAsync("canton7").Result;
            Console.WriteLine($"Name: {user.Name}. Blog: {user.Blog}. CreatedAt: {user.CreatedAt}");
            Console.WriteLine();

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            // The WriteDate method is where we'll make use
            // of our dependency injection. We'll define that
            // in a bit.
            WriteDate();

        }

        public static void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}
