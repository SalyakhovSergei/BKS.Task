namespace BKS.Task.RequestsModels;

/// <summary>
/// Model for delete messaged
/// </summary>
public class DeleteMessageRequestModel
{
    public int UserId { get; set; }
    public Guid MessageId { get; set; }
}