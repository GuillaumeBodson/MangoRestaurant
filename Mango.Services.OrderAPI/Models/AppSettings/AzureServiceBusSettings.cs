namespace Mango.Services.OrderAPI.Models.AppSettings
{
    public class AzureServiceBusSettings
    {
        public string ConnectionString { get; set; }
        public string CheckoutMessageTopic { get; set; }
        public string OrderPaymentProcessTopic { get; set; }
        public string SubscriptionName { get; set; }
    }
}
