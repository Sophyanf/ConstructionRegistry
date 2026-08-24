using ConstructionRegistry.Data;
using ConstructionRegistry.Services;
using ConstructionRegistry.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ConstructionRegistry
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // --- Регистрация сервисов (подставь свои интерфейсы и классы) ---
            services.AddSingleton<IKontragentService, KontragentService>();
            services.AddSingleton<IConstructionObjectService, ConstructionObjectService>();
            services.AddSingleton<IContractCustomerService, ContractCustomerService>();
            services.AddSingleton<IResponsiblPersonService, ResponsiblPersonService>();

            // Если у тебя есть DbContext — регистрируй его здесь
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Твоя_Строка_Подключения"));

            // Другие сервисы...
            services.AddSingleton<IContractCustomerService, ContractCustomerService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
