
using Infrastructuur.WebScrapper;
using WebScrapperPdf.Services.Classes;
using WebScrapperPdf.Services.Interfaces;

namespace WebScrapperPdf.Bootstrapper
{
    public static class BootstrapService
    {
        public static IServiceCollection AddWebScrapperServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IDataService, DataService>();
        }
    }
}
