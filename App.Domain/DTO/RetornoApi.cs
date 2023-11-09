using Newtonsoft.Json;

namespace App.Domain.DTOs
{
    public class RetornoApi
    {
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public static RetornoApi Sucesso(object data = null, string token = "")
        {
            return new RetornoApi()
            {
                Status = "success",
                Data = data,
                Token = token
            };
        }

        public static RetornoApi Erro(string message)
        {
            return new RetornoApi()
            {
                Status = "error",
                Message = message
            };
        }
    }
}