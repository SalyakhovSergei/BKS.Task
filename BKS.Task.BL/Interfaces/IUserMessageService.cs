using BKS.Task.BL.Models;

namespace BKS.Task.BL.Interfaces;

public interface IUserMessageService
{
    Task<AddMessageResponseModel> AddMessage(int userId, string message);
    Task<GetUserMessageResponseModel> GetLastMessageByUserId(int userId);
    Task<IEnumerable<GetUserMessageResponseModel>> GetMessagesByUserId(int userId);
    Task<int> DeleteMessage(int userId, Guid messageId);
}