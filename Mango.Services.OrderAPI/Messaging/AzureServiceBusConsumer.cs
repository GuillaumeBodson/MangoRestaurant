using Azure.Messaging.ServiceBus;
using Mango.Services.OrderAPI.Helpers;
using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Models.AppSettings;
using Mango.Services.OrderAPI.Repository;
using Microsoft.Extensions.Options;
using System.Text;

namespace Mango.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly OrderRepository _orderRepository;
        private readonly AzureServiceBusSettings _azureServiceBusSettings;

        private ServiceBusProcessor _checkoutProcessor;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IOptions<AzureServiceBusSettings> azureOptions)
        {
            _orderRepository = orderRepository;
            _azureServiceBusSettings = azureOptions.Value;
            var client = new ServiceBusClient(_azureServiceBusSettings.ConnectionString);
            _checkoutProcessor = client.CreateProcessor(_azureServiceBusSettings.CheckoutMessageTopic, _azureServiceBusSettings.SubscriptionName);
        }
        public async Task Start()
        {
            _checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            _checkoutProcessor.ProcessErrorAsync += ErrorHandler;
            await _checkoutProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _checkoutProcessor.StopProcessingAsync();
            await _checkoutProcessor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var body = Encoding.UTF8.GetString(args.Message.Body);

            CheckoutHeaderDto checkoutHeader = JsonHelper.DeserializeIgnoringCase<CheckoutHeaderDto>(body);

            //TODO : add mapping profile
            OrderHeader orderHeader = new()
            {
                CartNumber = checkoutHeader.CartNumber,
                CartTotalItems = checkoutHeader.CartTotalItems,
                CouponCode = checkoutHeader.CouponCode,
                CVV = checkoutHeader.CVV,
                DiscountTotal = checkoutHeader.DiscountTotal,
                Email = checkoutHeader.Email,
                ExpiryMonthYear = checkoutHeader.ExpiryMonthYear,
                FirstName = checkoutHeader.FirstName,
                LastName = checkoutHeader.LastName,
                PickupDate = checkoutHeader.PickupDate,
                Phone = checkoutHeader.Phone,
                UserId = checkoutHeader.UserId,
                OrderDetails = checkoutHeader.CartDetails.Select(x => new OrderDetails()
                {
                    Count = x.Count,
                    ProductId = x.ProductId,
                    ProductPrice = x.Product.Price,
                    ProductName = x.Product.Name,
                }).ToList(),
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeader.OrderTotal,
                PaymenStatus = false
            };
            orderHeader.CartTotalItems = orderHeader.OrderDetails.Sum(x => x.Count);

            await _orderRepository.AddOrder(orderHeader);
        }
    }
}
