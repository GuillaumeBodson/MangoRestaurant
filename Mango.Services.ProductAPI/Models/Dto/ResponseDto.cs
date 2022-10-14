using Mango.Services.ProductAPI.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Mango.Services.ProductAPI.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSucces { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }

        public T GetResult<T>()
        {
            if (!IsSucces)
            {
                return default;
            }

            return JsonHelper.DeserializeIgnoringCase<T>(Convert.ToString(Result));
        }

        public void SetFailure(Exception ex)
        {
            IsSucces = false;
            ErrorMessages = new List<string>() { ex.ToString() };
        }

        public async Task SetResult(Func<Task<object>> func)
        {
            try
            {
                Result = await func();
            }
            catch (Exception ex)
            {
                SetFailure(ex);
            }
        }
    }
}
