using AutoMapper;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Requests.Users;

namespace HomeControl.AccessControl.WebApi.Infrastructure.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserPostRequest, User>();
            CreateMap<UserPutRequest, User>();
            CreateMap<User, UserPostResponse>();
        }
    }
}
