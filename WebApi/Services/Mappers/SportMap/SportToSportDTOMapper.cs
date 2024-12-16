using WebApi.Models.DTO;
using WebApi.Models;

namespace WebApi.Services.Mappers.SportMap
{
    public class SportToSportDTOMapper : CustomMapper<Sport, SportDTO>
    {
        public override SportDTO Map(Sport source)
        {
            return new SportDTO
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
            };
        }
    }
}
