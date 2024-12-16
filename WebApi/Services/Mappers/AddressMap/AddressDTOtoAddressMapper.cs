using WebApi.Models.DTO;
using WebApi.Models;

namespace WebApi.Services.Mappers.AddressMap
{
    public class AddressDTOtoAddressMapper : CustomMapper<AddressDTO, Address>
    {
        public override Address Map(AddressDTO source)
        {
            return new Address
            {
                Id = source.Id,
                Road = source.Road,
                City = source.City,
                Zipcode = source.Zipcode
            };
        }
    }
}
