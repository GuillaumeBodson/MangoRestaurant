using Mango.Services.OrderAPI.Messaging;

namespace Mango.Services.OrderAPI.Helpers
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            var serviceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifeTime.ApplicationStarted.Register(async () => await serviceBusConsumer.Start());
            hostApplicationLifeTime.ApplicationStopped.Register(async () => await serviceBusConsumer.Stop());

            return app;
        }
    }
}
