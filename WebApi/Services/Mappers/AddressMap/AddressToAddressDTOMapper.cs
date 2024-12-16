
using WebApi.Models.DTO;
using WebApi.Models;

namespace WebApi.Services.Mappers.AddressMap
{
    public class AddressToAddressDTOMapper : CustomMapper<Address, AddressDTO>
    {
        public override AddressDTO Map(Address source)
        {
            return new AddressDTO
            {
                Id = source.Id,
                Road = source.Road,
                City = source.City,
                Zipcode = source.Zipcode
            };
        }
    }
}
