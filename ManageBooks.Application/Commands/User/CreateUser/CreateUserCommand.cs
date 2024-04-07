using ManageBooks.Domain.Entity;
using MediatR;

namespace ManageBooks.Application.Commands.User.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public List<Assessment> Assessments { get; set; }

    public CreateUserCommand(
        string email, 
        string name, 
        DateTime createdAt, 
        bool isActive, 
        List<Assessment> assessments
    )
    {
        Email = email;
        Name = name;
        CreatedAt = createdAt;
        IsActive = isActive;
        Assessments = assessments;
    }
}
