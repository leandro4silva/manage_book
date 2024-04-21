using DomainEntity = ManageBooks.Domain.Entity;
using ManageBooks.Infrastructure;
using MediatR;
using ManageBooks.Application.Interfaces;
using ManageBooks.Domain.Repository;

namespace ManageBooks.Application.Commands.User.CreateUser;

public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandle(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateUserCommand request, 
        CancellationToken cancellationToken
    )
    {
        var user = new DomainEntity.User(
            request.Email,
            request.Name,
            request.IsActive
        );

        await _userRepository.Insert(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return user.Id;
    }
}
