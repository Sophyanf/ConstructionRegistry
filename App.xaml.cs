using ConstructionRegistry.Data;
using ConstructionRegistry.Services;
using ConstructionRegistry.Services.Impl;
using ConstructionRegistry.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
namespace ConstructionRegistry
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static T GetService<T>() where T : notnull
        {
            return ServiceProvider.GetRequiredService<T>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            // DbContext как Transient
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConstructionRegistryDb;Trusted_Connection=True;MultipleActiveResultSets=true"),
                ServiceLifetime.Transient);
            services.AddTransient<IConstructionObjectService, ConstructionObjectService>();
            services.AddTransient<IResponsiblPersonService, ResponsiblPersonService>();
            services.AddTransient<IKontragentService, KontragentService>();
            services.AddTransient<IWindowNavigator, WindowNavigator>();
            ServiceProvider = services.BuildServiceProvider();
            var mainView = new MainView();
            mainView.Show();
        }
    }
}