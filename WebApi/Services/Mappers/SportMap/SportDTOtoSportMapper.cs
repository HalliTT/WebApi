using WebApi.Models.DTO;
using WebApi.Models;

namespace WebApi.Services.Mappers.SportMap
{
    public class SportDTOtoSportMapper : CustomMapper<SportDTO, Sport>
    {
        public override Sport Map(SportDTO source)
        {
            return new Sport
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
            };
        }
    }
}
