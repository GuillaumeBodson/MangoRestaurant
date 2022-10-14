using Mango.web.Models;
using Mango.web.Models.Factories;

namespace Mango.web.services
{
    public class CouponService : BaseService, ICouponService
    {
        public CouponService(IHttpClientFactory httpClient, IHttpRequestMessageFactory requestMessageFactory) : base(httpClient, requestMessageFactory)
        {

        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.Get,
                AccessToken = token,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
            });
        }
    }
}
