using Mango.web.Models;

namespace Mango.web.services.Iservices
{
    public interface ICartService
    {
        Task<T> AddToCart<T>(CartDto cartDto, string token = null);
        Task<T> GetCartByUserId<T>(string userId, string token = null);
        Task<T> RemoveFromCart<T>(int cartId, string token = null);
        Task<T> UpdateCart<T>(CartDto cartDto, string token = null);
        Task<CartDto> BuildCartDto(ProductDto product);
    }
}