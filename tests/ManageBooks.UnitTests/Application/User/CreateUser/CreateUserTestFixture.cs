using ManageBooks.Application.Commands.User.CreateUser;
using ManageBooks.UnitTests.Application.Common;

namespace ManageBooks.UnitTests.Application.User.CreateUser;

public class CreateUserTestFixture : UserBaseFixture
{
    public CreateUserTestFixture() : base() { }

    public CreateUserCommand GetValidInput()
    {
        return new CreateUserCommand(
             GetValidUserEmail(),
             GetValidUserName()
        );
    }

    public CreateUserCommand GetInvalidInputLongName()
    {
        var invalidInputTooLongName = GetValidInput();
        var tooLongNameForUser = "";

        while (tooLongNameForUser.Length < 255)
        {
            tooLongNameForUser = $"{tooLongNameForUser} {Faker.Name.FirstName()}";
        }

        invalidInputTooLongName.Name = tooLongNameForUser;

        return invalidInputTooLongName;
    }

    public CreateUserCommand GetInvalidInputNameNull()
    {
        var invalidInputTooNullName = GetValidInput();
        invalidInputTooNullName.Name = null!;

        return invalidInputTooNullName;
    }

    public CreateUserCommand GetInvalidInputEmailNull()
    {
        var invalidInputTooNullName = GetValidInput();
        invalidInputTooNullName.Email = null!;

        return invalidInputTooNullName;
    }

    public CreateUserCommand GetInvalidInputEmailIsNotValid()
    {
        var invalidInputWithInvalidEmail = GetValidInput();
        invalidInputWithInvalidEmail.Email = Faker.Name.FirstName();

        return invalidInputWithInvalidEmail;
    }

    [CollectionDefinition(nameof(CreateUserTestFixture))]
    public class CreateUserTestFixtureColletion 
        : ICollectionFixture<CreateUserTestFixture> { }
}
