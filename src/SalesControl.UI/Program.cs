using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using SalesControl.Application.Clients.Queries;           // para referenciar o assembly da Application
using SalesControl.Domain.ClientAggregate;
using SalesControl.Domain.ProductAggregate;
using SalesControl.Domain.SaleAggregate;
using SalesControl.Infrastructure.Persistence;
using SalesControl.Infrastructure.Repositories;
using System.Windows.Forms;

namespace SalesControl.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var host = CreateHostBuilder().Build();
                var mainForm = host.Services.GetRequiredService<FormMain>();
                System.Windows.Forms.Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro crítico ao iniciar a aplicação:\n\n{ex.Message}\n\n{ex.InnerException?.Message}",
                    "Erro de Inicialização", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(
                            context.Configuration.GetConnectionString("DefaultConnection")
                        )
                    );

                    services.AddScoped(
                        typeof(SalesControl.Application.Interfaces.IReadRepository<>),
                        typeof(SalesControl.Infrastructure.Repositories.EfRepository<>)
                    );

                    services.AddScoped(
                        typeof(SalesControl.Application.Interfaces.IRepository<>),
                        typeof(SalesControl.Infrastructure.Repositories.EfRepository<>)
                    );

                    // 4. Forms como Transient (cada abertura é uma nova instância)
                    services.AddTransient<FormMain>();
                    services.AddTransient<FormClientes>();
                    services.AddTransient<FormProdutos>();
                    services.AddTransient<FormVendas>();
                    // services.AddTransient<FormRelatorio>(); // ← adicione se tiver
                });
    }
}