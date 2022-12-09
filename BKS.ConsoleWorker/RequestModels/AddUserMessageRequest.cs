using Newtonsoft.Json;

namespace BKS.ConsoleWorker.RequestModels;

public class AddUserMessageRequest
{
    [JsonProperty("userId")]
    public int UserId { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
}