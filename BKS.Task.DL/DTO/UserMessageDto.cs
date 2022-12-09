using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BKS.Task.DL.DTO;

/// <summary>
/// Data transfer object for user messages
/// </summary>
[Table("UserMessages")]
public class UserMessageDto
{
    public UserMessageDto(int userId, string message)
    {
        Message = message;
        UserId = userId;
    }
    [Key]
    internal int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
    public DateTime RecordDate { get; set; }
    public Guid MessageId { get; set; }
}