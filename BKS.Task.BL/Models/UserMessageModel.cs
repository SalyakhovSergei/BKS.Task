namespace BKS.Task.BL.Models;

/// <summary>
/// Model that used in business logic
/// </summary>
public class UserMessageModel
{
    public int UserID { get; set; }
    public string Message { get; set; }
    public DateTime RecordDate { get; set; }
    public Guid MessageId { get; set; }
}