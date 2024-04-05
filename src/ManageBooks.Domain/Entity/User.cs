using ManageBooks.Domain.Validation;
using SeedWork = ManageBooks.Domain.SeedWork;

namespace ManageBooks.Domain.Entity;

public class User : SeedWork.AggregateRoot
{
    public string Email { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public List<Assessment> Assessments { get; private set; }
    
    public User(string email, string name, bool isActive = true)
    {
        Email = email;
        Name = name;
        Assessments = new List<Assessment>();
        CreatedAt = DateTime.Now;
        IsActive = isActive;

        Validate();
    }

    public void Validate()
    {
        DomainValidation.ValidEmail(Email, nameof(Email));
        DomainValidation.NotNullOrEmpty(Email, nameof(Email));

        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));
    }
}
