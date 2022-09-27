using Mango.web.Models;

namespace Mango.web.services.Iservices
{
    public interface IBaseService:IDisposable
    {
        public ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
