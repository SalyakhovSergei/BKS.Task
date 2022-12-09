using AutoMapper;
using BKS.Task.BL.Interfaces;
using BKS.Task.BL.Models;
using BKS.Task.DL.DTO;
using BKS.Task.DL.Interfaces;

namespace BKS.Task.BL.Services;

/// <summary>
/// Service that contains business logic
/// </summary>
public class UserMessageService: IUserMessageService
{
    private readonly IUserMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public UserMessageService(IUserMessageRepository messageRepository, 
                                IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<AddMessageResponseModel> AddMessage(int userId, string message)
    {
        var messageId = Guid.NewGuid();
        var result = await _messageRepository.AddMessage(userId, message, messageId);

        var response = new AddMessageResponseModel
        {
            MessageId = messageId,
            State = ResponseCodes.Success
        };

        return response;
    }

    public async Task<GetUserMessageResponseModel> GetLastMessageByUserId(int userId)
    {
        var dto = await _messageRepository.GetLastMessageByUserId(userId);
        var model = _mapper.Map<GetUserMessageResponseModel>(dto);
        return model;
    }

    public async Task<IEnumerable<GetUserMessageResponseModel>> GetMessagesByUserId(int userId)
    {
        var messages = await _messageRepository.GetMessagesByUserId(userId);
        var models = messages.Select(dto => _mapper.Map<GetUserMessageResponseModel>(dto));
        return models;
    }

    public Task<int> DeleteMessage(int userId, Guid messageId)
    {
        return _messageRepository.DeleteMessage(userId, messageId);
    }
}