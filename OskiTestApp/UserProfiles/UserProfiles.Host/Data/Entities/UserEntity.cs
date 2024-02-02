namespace UserProfiles.Host.Data.Entities
{
    public class UserEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
