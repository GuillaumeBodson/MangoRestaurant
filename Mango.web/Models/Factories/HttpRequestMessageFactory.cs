using System.Reflection.PortableExecutable;
using static Mango.web.SD;

namespace Mango.web.Models.Factories
{
    public interface IHttpRequestMessageFactory
    {
        HttpRequestMessage Create(ApiType apiType, string url);
    }

    public class HttpRequestMessageFactory : IHttpRequestMessageFactory
    {
        private readonly Func<HttpRequestMessage> _factoryMethod;

        public HttpRequestMessageFactory(Func<HttpRequestMessage> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        public HttpRequestMessage Create(ApiType apiType, string url)
        {
            var res = _factoryMethod();

            res.Headers.Add("Accept", "application/json");
            res.RequestUri = new(url);

            res.Method = apiType switch
            {
                ApiType.Post => HttpMethod.Post,
                ApiType.Put => HttpMethod.Put,
                ApiType.Delete => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };

            return res;
        }
    }
}
