namespace BKS.Task.RequestsModels;

/// <summary>
/// Model for post messaged
/// </summary>
public class PostMessageRequest
{
    public int UserId { get; set; }
    public string Message { get; set; }
}