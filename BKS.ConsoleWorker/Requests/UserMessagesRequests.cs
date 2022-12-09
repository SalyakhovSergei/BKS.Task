using System.Data.SqlTypes;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using BKS.ConsoleWorker.Entities;
using BKS.ConsoleWorker.RequestModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BKS.ConsoleWorker.Requests;

/// <summary>
/// Class that makes requests to API
/// </summary>
public class UserMessagesRequests
{
    private readonly ApiUrls? _apiUrls;
    public UserMessagesRequests()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsetings.json", false, true)
            .Build();

        _apiUrls = config.GetSection("ApiUrls").Get<ApiUrls>();
    }


    public async Task<string?> GetLastUserMessage(int userId)
    {
        string? result;
        using (var hc = new HttpClient())
        {
            string url = $@"{_apiUrls.GetLastUserMessage}{userId}";
            result = await hc.GetStringAsync(url);
        }

        return result;
    }

    public async Task<string?> GetUserMessages(int userId)
    {
        string? result;
        using (var hc = new HttpClient())
        {
            string url = $@"{_apiUrls.GetUserMessages}{userId}";
            result = await hc.GetStringAsync(url);
        }

        return result;
    }

    public async Task<string?> AddUserMessage(int userId, string message)
    {
        HttpResponseMessage response;
        string url = _apiUrls.AddUserMessage;

        var request = new AddUserMessageRequest
        {
            UserId = userId,
            Message = message
        };
        var requestJson = JsonConvert.SerializeObject(request);
        var data = new StringContent(requestJson, Encoding.UTF8, "application/json");

        using (var hc = new HttpClient())
        {
            response = await hc.PostAsync(url, data);
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<HttpResponseMessage?> DeleteMessageByIdAndUserId(int userId, Guid messageId)
    {
        HttpResponseMessage? response;
        string url = _apiUrls.DeleteMessageByIdAndUserId;

        var request = new DeleteMessageRequest
        {
            UserId = userId,
            MessageId = messageId
        };
        var requestJson = JsonConvert.SerializeObject(request);
        var data = new StringContent(requestJson, Encoding.UTF8, "application/json");

        HttpRequestMessage req = new HttpRequestMessage
        {
            Content = JsonContent.Create(data),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(url)
        };

        using (var hc = new HttpClient())
        {
            response = await hc.SendAsync(req);
        }

        return response;
    }
}