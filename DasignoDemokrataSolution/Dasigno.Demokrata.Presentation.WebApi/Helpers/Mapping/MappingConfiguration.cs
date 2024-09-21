using AutoMapper;
using Dasigno.Demokrata.Core.Domain.Entities;
using Dasigno.Demokrata.Presentation.WebApi.ApiModels.Requests;
using Dasigno.Demokrata.Presentation.WebApi.ApiModels.Responses;

namespace Dasigno.Demokrata.Presentation.WebApi.Helpers.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<User, UserResponseModel>().ReverseMap();
            CreateMap<User, UserRequestModel>().ReverseMap();
        }

        public static List<UserResponseModel> ConvertListUsers(IMapper mapper, List<User> users)
        {
            return mapper.Map<List<UserResponseModel>>(users);
        }

        public static User ConvertUser(IMapper mapper, UserRequestModel userModel)
        {
            return mapper.Map<User>(userModel);
        }

        public static UserResponseModel ConvertUser(IMapper mapper, User user)
        {
            return mapper.Map<UserResponseModel>(user);
        }
    }
}
