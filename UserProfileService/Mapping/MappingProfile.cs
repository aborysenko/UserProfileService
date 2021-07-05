using UserProfileService.Data.Models;
using UserProfileService.Domain.Commands;

namespace UserProfileService.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(
                    x => x.Avatar, 
                    opt => opt.Ignore());

            CreateMap<UpdateUserCommand, User>()
                .ForMember(
                    x => x.Avatar,
                    opt => opt.Ignore());
        }
    }
}
