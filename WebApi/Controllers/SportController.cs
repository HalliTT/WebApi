using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models.DTO;
using WebApi.Models;
using WebApi.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using System.Net;

namespace WebApi.Controllers
{
    public class SportController : Controller
    {
        private readonly MembershipContext _context;
        private readonly ICustomMapper<Sport, SportDTO> _sportMapper;
        private readonly ICustomMapper<SportDTO, Sport> _sportDTOMapper;

        public SportController(
            MembershipContext context,
            ICustomMapper<Sport, SportDTO> sportMapper,
            ICustomMapper<SportDTO, Sport> sportDTOMapper
            )
        {
            _context = context;
            _sportMapper = sportMapper;
            _sportDTOMapper = sportDTOMapper;
        }

        // GET: Sport
        [HttpGet("GetSport")]
        public async Task<IActionResult> GetSport()
        {
            try
            {
                IEnumerable<Sport> SportList = new List<Sport>();
                SportList = await _context.Sports.ToListAsync();

                ValidationHelper.CheckForNull(SportList, nameof(SportList));

                var sportDTOs = _sportMapper.Map(SportList);

                return Ok(sportDTOs);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpGet("GetSport/{id}")]
        public async Task<IActionResult> GetSportById(int id)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            try
            {
                Sport Sport_Object = new Sport();
                Sport_Object = await _context.Sports.FindAsync(id);

                ValidationHelper.CheckForNull(Sport_Object, nameof(Sport_Object));

                var sportDTO = _sportMapper.Map(Sport_Object);

                return Ok(sportDTO);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpPost("CreateSport")]
        public async Task<IActionResult> CreateSport([FromBody] SportDTO sportDTO)
        {
            ValidationHelper.CheckForNull(sportDTO, nameof(sportDTO));
            ValidationHelper.CheckForNull(sportDTO.Name, nameof(sportDTO.Name));
            ValidationHelper.CheckForNull(sportDTO.Price, nameof(sportDTO.Price));

            try
            {
                Sport sport = new Sport();
                sport = _sportDTOMapper.Map(sportDTO);

                await _context.Sports.AddAsync(sport);
                var numberCreated = await _context.SaveChangesAsync();

                ValidationHelper.CheckForNull(numberCreated, nameof(numberCreated));

                var createdSport = _sportMapper.Map(sport);

                return Ok(createdSport);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpPut("UpdateSport/{id}")]
        public async Task<IActionResult> UpdateSport(int id, [FromBody] SportDTO sportDTO)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            ValidationHelper.CheckForNull(sportDTO, nameof(sportDTO));
            ValidationHelper.CheckForNull(sportDTO.Name, nameof(sportDTO.Name));
            ValidationHelper.CheckForNull(sportDTO.Price, nameof(sportDTO.Price));

            try
            {
                Sport ExistingSport = new Sport();
                ExistingSport = await _context.Sports.FindAsync(id);
                
                ValidationHelper.CheckForNull(ExistingSport, nameof(ExistingSport));

                Sport sport = _sportDTOMapper.Map(sportDTO);
                ExistingSport.Name = sport.Name;
                ExistingSport.Price = sport.Price;

                _context.Sports.Update(ExistingSport);
                await _context.SaveChangesAsync();

                var updatedSport = _sportMapper.Map(ExistingSport);

                return Ok(sport);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpDelete("DeleteSport/{id}")]
        public async Task<IActionResult> DeleteSport(int id)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            try
            {
                Sport ExistingSport = new Sport();
                ExistingSport = await _context.Sports.FindAsync(id);

                ValidationHelper.CheckForNull(ExistingSport, nameof(ExistingSport));

                _context.Sports.Remove(ExistingSport);
                var numberDeleted = await _context.SaveChangesAsync();

                ValidationHelper.CheckForNull(numberDeleted, nameof(numberDeleted));

                return Ok(numberDeleted);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }
    }
}
