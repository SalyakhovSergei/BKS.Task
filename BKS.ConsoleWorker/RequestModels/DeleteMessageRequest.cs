using Newtonsoft.Json;

namespace BKS.ConsoleWorker.RequestModels;

public class DeleteMessageRequest
{
    [JsonProperty("userId")]
    public int UserId { get; set; }
    [JsonProperty("messageId")]
    public Guid MessageId { get; set; }
}