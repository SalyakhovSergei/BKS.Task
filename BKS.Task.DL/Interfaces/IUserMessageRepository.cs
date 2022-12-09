using BKS.Task.DL.DTO;

namespace BKS.Task.DL.Interfaces;

public interface IUserMessageRepository
{
    Task<int> AddMessage(int userId, string message, Guid messageId);
    Task<UserMessageDto?> GetLastMessageByUserId(int userId);
    Task<List<UserMessageDto>> GetMessagesByUserId(int userId);
    Task<int> DeleteMessage(int userId, Guid messageId);
}