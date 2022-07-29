using EmailService.Middlewares;
using EmailService.Services.Initialization;
using EmailService.Services.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Options;
using RTUITLab.EmailService.Client;

namespace EmailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<EmailServiceOptions>(Configuration.GetSection(nameof(EmailServiceOptions)));
            services.AddHostedService<SmtpOptionsValidator>();

            services.AddSingleton<ILogsWebSocketHandler>(LogsWebSocketHandler.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            var logsToken = Configuration.GetValue<string>("LOGS_ACCESS_TOKEN");
            app.UseLogsMiddleware("/api/logsStream", logsToken);

            app.UseHeaderAuthorization(Configuration
                .GetSection(nameof(HeaderAuthorizationOptions))
                .Get<HeaderAuthorizationOptions>()
                .Key);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
