using BKS.Task.BL.Interfaces;
using BKS.Task.RequestsModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BKS.Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessageService _messageService;

        public UserMessagesController(IUserMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("addMessage")]
        public async Task<IActionResult> AddMessage([FromBody] PostMessageRequest request)
        {
            try
            {
                Log.Information($"Received AddMessage request for userId={request.UserId}");

                var response = await _messageService.AddMessage(request.UserId, request.Message);
                return Ok(response);

            }
            catch (Exception e)
            {
                Log.Error($"Error while posting message for userId={request.UserId}: {e.Message}");
            }

            return Ok();
        }

        [HttpGet("getLastMessage/{userId}")]
        public async Task<IActionResult> GetLastMessage(int userId)
        {
            Log.Information($"Received GetLastMessage request for userId={userId}");
            try
            {
                var response = await _messageService.GetLastMessageByUserId(userId);
                if (response?.Message is null)
                {
                    Log.Information($"No messages for userId={userId}");
                    return StatusCode(204);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error($"Error while getting message for userId={userId}: {e.Message}");
            }

            return Ok();
        }

        [HttpGet("getMessages/{userId}")]
        public async Task<IActionResult> GetMessages(int userId)
        {
            Log.Information($"Received GetMessages request for userId={userId}");
            try
            {
                var response = await _messageService.GetMessagesByUserId(userId);
                if (!response.Any())
                {
                    Log.Information($"No messages for userId={userId}");
                    return StatusCode(204);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error($"Error while getting messages for userId={userId}: {e.Message}");
            }

            return Ok();
        }

        [HttpDelete("deleteMessage")]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageRequestModel model)
        {
            Log.Information($"Received DeleteMessage request for userId={model.UserId} and messageId={model.MessageId}");
            try
            {
                var response = await _messageService.DeleteMessage(model.UserId, model.MessageId);
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error($"Error while deleting message for userId={model.UserId} and messageId= {e.Message}");
            }

            return Ok();
        }
    }
}
