using demoBlazor.Helpers;
using demoBlazor.Services.Cateogory;
using demoBlazor.Services.Product;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace demoBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7091") });
            builder.Services.AddScoped<JsHelper>();

            builder.Services.AddScoped<ICateogryClient, CategoryClient>();
            builder.Services.AddScoped<IProductClient, ProductClient>();



            builder.Services.AddFluxor(o =>
            {
                o.ScanAssemblies(typeof(Program).Assembly);

                o.UseReduxDevTools(rdt =>
                {
                    rdt.Name = "My application";
                });
            });



            builder.Services.AddBlazorBootstrap();

            await builder.Build().RunAsync();
        }
    }
}