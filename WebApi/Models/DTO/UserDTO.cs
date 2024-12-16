namespace WebApi.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegNumber { get; set; }
        public AddressDTO Address { get; set; }
    }
}
