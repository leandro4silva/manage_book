using MediatR;

namespace ManageBooks.Application.Commands.User.CreateUser;

public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, Guid>
{

    public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
