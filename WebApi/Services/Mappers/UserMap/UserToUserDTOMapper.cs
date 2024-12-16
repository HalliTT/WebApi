using WebApi.Models;
using WebApi.Models.DTO;

namespace WebApi.Services.Mappers.UserMap
{
    public class UserToUserDTOMapper : CustomMapper<User, UserDTO>
    {
        private readonly ICustomMapper<Address, AddressDTO> _addressMapper;
        public UserToUserDTOMapper(ICustomMapper<Address, AddressDTO> addressMapper)
        {
            _addressMapper = addressMapper;
        }

        public override UserDTO Map(User source)
        {
            return new UserDTO
            {
                Id = source.Id,
                Name = source.Name,
                RegNumber = source.RegNumber,
                Address = source.Address != null ? _addressMapper.Map(source.Address) : null
            };
        }
    }
}
