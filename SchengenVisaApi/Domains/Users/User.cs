namespace Domains.Users
{
    public class User : IEntity
    {
        /// Unique Identifier of <see cref="User"/>
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Role Role { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
