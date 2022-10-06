using Mango.web.Models;
using System.Text.Json;

namespace Mango.web.Helpers
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
    }
}
