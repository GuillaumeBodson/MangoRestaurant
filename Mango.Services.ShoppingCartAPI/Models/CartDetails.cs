using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        public int CartDetailsId { get; set; }
        public int HeaderId { get; set; }
        [ForeignKey(nameof(HeaderId))]
        public virtual CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
