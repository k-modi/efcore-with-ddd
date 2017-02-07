namespace efcore2_webapi.Models
{
    public class UserDto : EntityDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
    }
}