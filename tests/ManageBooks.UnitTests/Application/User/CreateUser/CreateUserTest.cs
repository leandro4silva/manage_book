using FluentAssertions;
using ManageBooks.Application.Commands.User.CreateUser;
using ManageBooks.Domain.Exceptions;
using Moq;
using Command = ManageBooks.Application.Commands.User.CreateUser;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Application.User.CreateUser;

[Collection(nameof(CreateUserTestFixture))]
public class CreateUserTest
{
    private readonly CreateUserTestFixture _createUserTestFixture;

    public CreateUserTest(CreateUserTestFixture createUserTestFixture)
    {
        _createUserTestFixture = createUserTestFixture;
    }

    [Fact(DisplayName = nameof(CreateUser))]
    [Trait("Application", "CreateUser - Commands")]
    public async void CreateUser()
    {
        var repositoryMock = _createUserTestFixture.GetRepositoryMock();
        var unitOfWorkMock = _createUserTestFixture.GetUnitOfWorkMock();

        var command = new Command.CreateUserCommandHandle(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _createUserTestFixture.GetValidInput();

        var output = await command.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        output.Should().NotBeEmpty();
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateUser))]
    [Trait("Application", "CreateUser - Commands")]
    [MemberData(
        nameof(CreateUserTestDataGenerator.GetInvalidInputs),
        parameters:24,
        MemberType = typeof(CreateUserTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiateUser(CreateUserCommand input, string exceptionMessage)
    {
        var useCase = new Command.CreateUserCommandHandle(
            _createUserTestFixture.GetRepositoryMock().Object,
            _createUserTestFixture.GetUnitOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }
}
