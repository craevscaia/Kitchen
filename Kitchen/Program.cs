using Kitchen;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services); // calling ConfigureServices method

        var app = builder.Build();
        Startup.Configure(app, builder.Environment); // calling Configure method
    }
}