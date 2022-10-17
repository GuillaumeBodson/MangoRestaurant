using Mango.web.Helpers;
using System.Text.Json;

namespace Mango.web.Models
{
    public class ResponseDto
    {
        public bool IsSucces { get; set; }
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

        public async Task SetResult<T>(Func<Task<T>> func)
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
        public static ResponseDto NewErrorResponse(Exception ex)
        {
            var response = new ResponseDto
            {
                DisplayMessage = "Error",
            };
            response.SetFailure(ex);
            return response;
        }
    }
}
