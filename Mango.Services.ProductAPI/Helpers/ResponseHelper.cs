using Mango.Services.ProductAPI.Models.Dto;
using System.Text.Json;

namespace Mango.Services.ProductAPI.Helpers
{
    public static class ResponseHelper
    {
        public static T GetResult<T>(this ResponseDto response)
        {
            if (response == null || !response.IsSucces)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(Convert.ToString(response.Result));
        }
        
        public static ResponseDto SetFailure(this ResponseDto response, Exception ex)
        {
            response.IsSucces = false;
            response.ErrorMessages = new List<string>() { ex.ToString() };
            return response;
        }

        public static async Task<ResponseDto> SetResult<T>(this ResponseDto _response, Func<Task<T>> func)
        {
            try
            {
                _response.Result = await func();
            }
            catch (Exception ex)
            {
                _response.SetFailure(ex);
            }
            return _response;
        }
    }
}
