using Kitchen.Kitchen;
using Kitchen.Repository.CookRepository;
using Kitchen.Repository.FoodRepository;
using Kitchen.Repository.OrderRepository;
using Kitchen.Services;
using Kitchen.Services.CookService;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;

namespace Kitchen;

public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRazorPages();
        services.AddSingleton<IFoodRepository, FoodRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<ICookRepository, CookRepository>();
        
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IFoodService, FoodService>();
        services.AddSingleton<ICookService, CookService>();
        
        services.AddSingleton<IKitchen, Kitchen.Kitchen>();
        services.AddHostedService<BackgroundTask.BackgroundTask>();
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}