using BKS.ConsoleWorker.Requests;
using Serilog;

namespace BKS.ConsoleWorker.HostedServices;

/// <summary>
/// Class for random calling API request methods
/// </summary>
public class RandomWorker
{
    private static UserMessagesRequests _messagesRequests;
    public RandomWorker()
    {
        _messagesRequests = new UserMessagesRequests();
    }
    public async Task<RandomWorker> DoRandomWork()
    {
        object? answer = null;
        var rnd = new Random();
        int method = rnd.Next(4);
        switch (method)
        {
            case 0:
                answer = await _messagesRequests.AddUserMessage(2, "Hi there");
                Log.Information("Add message request completed");
                break;
            case 1:
                answer = await _messagesRequests.GetLastUserMessage(2);
                Log.Information("Get message request completed");
                break;
            case 2:
                answer = await _messagesRequests.GetUserMessages(2);
                Log.Information("Get all messages request completed");
                break;
            case 3:
                answer = await _messagesRequests.DeleteMessageByIdAndUserId(4, Guid.Parse("902AD453-0910-4F7C-827F-9B4D4314B9BA"));
                Log.Information("Delete message request completed");
                break;

        }
        Log.Information(answer?.ToString() ?? "No answer");
        return (RandomWorker)answer!;
    }
}