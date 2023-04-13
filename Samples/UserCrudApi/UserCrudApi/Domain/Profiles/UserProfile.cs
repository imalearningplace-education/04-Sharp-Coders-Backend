using AutoMapper;
using Domain.Dto;
using Domain.Model;

namespace Domain.Profiles; 

public class UserProfile : Profile {
    public UserProfile() {
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserProfileDto>();
    }
}
