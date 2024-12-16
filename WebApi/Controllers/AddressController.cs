using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Net;
using System.Text.Json.Serialization;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.DTO;
using WebApi.Services.Mappers;


namespace WebApi.Controllers
{
    public class AddressController : Controller
    {
        private readonly MembershipContext _context;
        private readonly ICustomMapper<Address, AddressDTO> _addressMapper;
        private readonly ICustomMapper<AddressDTO, Address> _addressDTOMapper;

        public AddressController(
            MembershipContext context, 
            ICustomMapper<Address, AddressDTO> addressMapper,
            ICustomMapper<AddressDTO, Address> addressDTOMapper
            )
        {
            _context = context;
            _addressMapper = addressMapper;
            _addressDTOMapper = addressDTOMapper;
        }

        // GET: Address
        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress()
        {
            try
            {
                IEnumerable<Address> AddressList = new List<Address>();
                AddressList = await _context.Addresses.ToListAsync();

                ValidationHelper.CheckForNull(AddressList, nameof(AddressList));

                var addressDTOs = _addressMapper.Map(AddressList);

                return Ok(addressDTOs);
            }
            catch (Exception Error)
            {               
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpGet("GetAddress/{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            try
            {
                Address Address_Object = new Address();
                Address_Object = await _context.Addresses.FindAsync(id);

                ValidationHelper.CheckForNull(Address_Object, nameof(Address_Object));

                var AddressDTO = _addressMapper.Map(Address_Object);

                return Ok(AddressDTO);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDTO addressDTO)
        {
            ValidationHelper.CheckForNull(addressDTO, nameof(addressDTO));
            ValidationHelper.CheckForNullOrEmpty(addressDTO.Road, nameof(addressDTO.Road));
            ValidationHelper.CheckForNullOrEmpty(addressDTO.City, nameof(addressDTO.City));
            ValidationHelper.CheckForNullOrEmpty(addressDTO.Zipcode, nameof(addressDTO.Zipcode));

            try
            {
                Address Address_Object = new Address();
                Address_Object = _addressDTOMapper.Map(addressDTO);

                await _context.Addresses.AddAsync(Address_Object);
                var numberCreated = await _context.SaveChangesAsync();

                ValidationHelper.CheckForNull(numberCreated, nameof(numberCreated));

                var createdAddressDTO = _addressMapper.Map(Address_Object);

                return Ok(createdAddressDTO);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpPut("UpdateAddress/{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDTO addressDTO)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            ValidationHelper.CheckForNull(addressDTO, nameof(addressDTO));
            ValidationHelper.CheckForNullOrEmpty(addressDTO.Road, nameof(addressDTO.Road));
            ValidationHelper.CheckForNullOrEmpty(addressDTO.City, nameof(addressDTO.City));
            ValidationHelper.CheckForNullOrEmpty(addressDTO.Zipcode, nameof(addressDTO.Zipcode));

            try
            {
                Address ExistingAddress = new Address();
                ExistingAddress = await _context.Addresses.FindAsync(id);

                ValidationHelper.CheckForNull(ExistingAddress, nameof(ExistingAddress));

                Address address = _addressDTOMapper.Map(addressDTO);

                ExistingAddress.Road = address.Road;
                ExistingAddress.City = address.City;
                ExistingAddress.Zipcode = address.Zipcode;

                _context.Addresses.Update(ExistingAddress);
                await _context.SaveChangesAsync();

                var updatedAddressDTO = _addressMapper.Map(ExistingAddress);

                return Ok(updatedAddressDTO);
            }
            catch (Exception Error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error : {Error.ToString()}");
            }
        }

        [HttpDelete("DeleteAddress/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            ValidationHelper.CheckForValidId(id, nameof(id));
            try
            {
                Address Address_Object = new Address();
                Address_Object = await _context.Addresses.FindAsync(id);

                ValidationHelper.CheckForNull(Address_Object, nameof(Address_Object));

                _context.Addresses.Remove(Address_Object);
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
