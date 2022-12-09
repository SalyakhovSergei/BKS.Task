using BKS.Task.DL.DTO;
using BKS.Task.DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BKS.Task.DL.Repositories;

/// <summary>
/// Repository for user messages to work with database
/// </summary>
public class UserMessageRepository: IUserMessageRepository
{
    private readonly BKSContext _bksContext;

    public UserMessageRepository(BKSContext bksContext)
    {
        _bksContext = bksContext;
    }

    public async Task<int> AddMessage(int userId, string message, Guid messageId)
    {
        var userMessage = new UserMessageDto(userId, message)
        {
            RecordDate = DateTime.UtcNow,
            MessageId = messageId
        };

        var entry = _bksContext.Entry(userMessage);
        if (entry.State == EntityState.Detached)
        {
            await _bksContext.UserMessage.AddAsync(userMessage);
            Log.Information($"Message with Id:{messageId} for userId:{userId} added to database");
        }
        
        return await _bksContext.SaveChangesAsync();
    }

    public async Task<UserMessageDto?> GetLastMessageByUserId(int userId)
    {
        var message = _bksContext.UserMessage.Where(e=>e.UserId == userId)
            .OrderByDescending(e => e.RecordDate).FirstOrDefaultAsync();
        Log.Information($"Get request for last message for userId:{userId}");
        return await message;
    }

    public async Task<List<UserMessageDto>> GetMessagesByUserId(int userId)
    {
        var messages = _bksContext.UserMessage.Where(e => e.UserId == userId)
            .OrderByDescending(e => e.RecordDate).ToListAsync();
        Log.Information($"Get request for all messages for userId:{userId}");
        return await messages;
    }

    public async Task<int> DeleteMessage(int userId, Guid messageId)
    {
        var message = await _bksContext.UserMessage.Where(e => e.UserId == userId && e.MessageId == messageId).FirstOrDefaultAsync();
        if (message is null)
        {
            Log.Error($"No message with id:{messageId} and userId:{userId}");
            return ResponseCodes.Error;
        }
        await _bksContext.UserMessage.Where(e => e.UserId == userId && e.MessageId == messageId).ExecuteDeleteAsync();
        await _bksContext.SaveChangesAsync();
        Log.Information($"Message with id:{messageId} and userId:{userId} deleted for database");
        return ResponseCodes.Error;
    }
}