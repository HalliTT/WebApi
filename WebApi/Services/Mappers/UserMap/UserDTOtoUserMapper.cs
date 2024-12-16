using WebApi.Models;
using WebApi.Models.DTO;

namespace WebApi.Services.Mappers.UserMap
{
    public class UserDTOtoUserMapper : CustomMapper<UserDTO, User>
    {
        private readonly ICustomMapper<AddressDTO, Address> _addressMapper;
        public UserDTOtoUserMapper(ICustomMapper<AddressDTO, Address> addressMapper)
        {
            _addressMapper = addressMapper;
        }
        public override User Map(UserDTO source)
        {
            return new User
            {
                Id = source.Id,
                Name = source.Name,
                RegNumber = source.RegNumber,
                AddressId = source.Address != null ? source.Address.Id : 0,
                Address = source.Address != null ? _addressMapper.Map(source.Address) : null
            };
        }
    }
}
