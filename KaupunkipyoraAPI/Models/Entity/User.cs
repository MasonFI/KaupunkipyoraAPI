using KaupunkipyoraAPI.Contracts;

namespace KaupunkipyoraAPI.Models.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime Created { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? Updated { get; set; }
        public int? UpdatedById { get; set; }
        public User? CreatedBy { get; set; } = default!;
        public User? UpdatedBy { get; set; }
    }
}
