using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models.DTO;
using WebApi.Models;
using WebApi.Services.Mappers;

namespace WebApi.Controllers
{
    public class MembershipController : Controller
    {
        private readonly MembershipContext _context;
        private readonly ICustomMapper<User, UserDTO> _userMapper;
        private readonly ICustomMapper<UserDTO, User> _userDTOMapper;


        public MembershipController(
            MembershipContext context,
            ICustomMapper<User, UserDTO> userMapper,
            ICustomMapper<UserDTO, User> userDTOMapper
            )
        {
            _context = context;
            _userMapper = userMapper;
            _userDTOMapper = userDTOMapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
