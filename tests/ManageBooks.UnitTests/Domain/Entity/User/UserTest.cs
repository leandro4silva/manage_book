using FluentAssertions;
using ManageBooks.Domain.Exceptions;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Domain.Entity.User;

public class UserTest
{
    private readonly UserTestFixture _userTestFixture;
    public UserTest()
    {
        _userTestFixture = new UserTestFixture();
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "User - Aggregate")]
    public void Instantiate()
    {
        var validUser = _userTestFixture.GetValidUser();

        var datetimeBefore = DateTime.Now;
        var user = new DomainEntity.User(
            validUser.Email,
            validUser.Name
        );
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Email.Should().Be(validUser.Email);
        user.Name.Should().Be(validUser.Name);
        user.CreatedAt.Should().NotBeSameDateAs(default);

        (user.CreatedAt >= datetimeBefore).Should().BeTrue();
        (user.CreatedAt <= datetimeAfter).Should().BeTrue();
        user.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenEmailIsWrong))]
    [Trait("Domain", "User - Aggregates")]
    [InlineData("example.com")]
    [InlineData("example@example")]
    [InlineData("example")]
    public void InstantiateErrorWhenEmailIsWrong(string? email)
    {
        var validUser = _userTestFixture.GetValidUser();

        Action action = () => new DomainEntity.User(email!, validUser.Name);
        action.Should().Throw<EntityValidationException>().WithMessage("Email should be a valid email");
    }
}
