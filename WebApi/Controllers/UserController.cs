using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models.DTO;
using WebApi.Models;
using WebApi.Services.Mappers;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi.Services.Mappers.UserMap;

namespace WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly MembershipContext _context;
        private readonly ICustomMapper<User, UserDTO> _userMapper;
        private readonly ICustomMapper<UserDTO, User> _userDTOMapper;

        public UserController(
            MembershipContext context,
            ICustomMapper<User, UserDTO> userMapper,
            ICustomMapper<UserDTO, User> userDTOMapper
            )
        {
            _context = context;
            _userMapper = userMapper;
            _userDTOMapper = userDTOMapper;
        }

        // GET: User
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                IEnumerable<User> UserList = new List<User>();
                UserList = await _context.Users.
                    Include(user => user.Address).
                    ToListAsync();

                ValidationHelper.CheckForNull(UserList, nameof(UserList));

                var userDTOs = _userMapper.Map(UserList);

                return Ok(userDTOs);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            try
            {
                User User_Object = new User();
                User_Object = await _context.Users.
                    Include(user => user.Address).
                    FirstOrDefaultAsync(user => user.Id == id);

                ValidationHelper.CheckForNull(User_Object, nameof(User_Object));


                var userDTO = _userMapper.Map(User_Object);

                return Ok(userDTO);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                ValidationHelper.CheckForNull(userDTO, nameof(userDTO));

                var user = _userDTOMapper.Map(userDTO);

                ValidationHelper.CheckForNull(user, nameof(user));

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                ValidationHelper.CheckForNull(userDTO, nameof(userDTO));
                
                var user = _userDTOMapper.Map(userDTO);

                ValidationHelper.CheckForNull(user, nameof(user));

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                ValidationHelper.CheckForValidId(id, nameof(id));

                var user = await _context.Users.FindAsync(id);

                ValidationHelper.CheckForNull(user, nameof(user));

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }
    }
}
