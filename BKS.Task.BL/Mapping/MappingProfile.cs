using AutoMapper;
using BKS.Task.BL.Models;
using BKS.Task.DL.DTO;

namespace BKS.Task.BL.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UserMessageDto, UserMessageModel>().ReverseMap();
        CreateMap<UserMessageDto, GetUserMessageResponseModel>().ReverseMap();
    }
}