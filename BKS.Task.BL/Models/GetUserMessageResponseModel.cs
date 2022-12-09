namespace BKS.Task.BL.Models;

/// <summary>
/// Response model for receiving messages
/// </summary>
public class GetUserMessageResponseModel
{
    public int UserID { get; set; }
    public string Message { get; set; }
}