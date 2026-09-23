using VaultLab.Domain.ValueObjects;

namespace VaultLab.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = String.Empty;
        public Email Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<Document> Documents { get; private set; }
        public User(Email email, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            CreatedAt = DateTime.UtcNow;
            Documents = [];
        }

    }
}