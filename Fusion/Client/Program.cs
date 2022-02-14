using Blazored.SessionStorage;
using DataTables.Blazor.Extensions;
using Fusion.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>();

            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddDataTables();

            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("LoggedIn", policy => policy.RequireClaim("LoggedIn", "true"));
                options.AddPolicy("AdminUser", policy => policy.RequireClaim("AdminUser", "true"));
                options.AddPolicy("RegularUser", policy => policy.RequireClaim("RegularUser", "true"));
                options.AddPolicy("SalesUser", policy => policy.RequireClaim("SalesUser", "true"));
            });

            await builder.Build().RunAsync();
        }
    }
}
