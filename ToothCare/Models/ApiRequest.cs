
using static Utility.SD;

namespace ToothCare.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; }
        public object ApiData { get; set; }
        public string ApiToken { get; set; }
    }
}
