namespace BKS.Task.BL.Models;
/// <summary>
/// Response model after adding messages
/// </summary>
public class AddMessageResponseModel
{
    public Guid MessageId { get; set; }
    public int State { get; set; }
}