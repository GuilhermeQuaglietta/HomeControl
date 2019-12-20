using AutoMapper;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Requests.Users;

namespace HomeControl.AccessControl.WebApi.Infrastructure.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserPutRequest, User>();
            CreateMap<UserPostRequest, User>();
            CreateMap<User, UserPutResponse>();
        }
    }
}
