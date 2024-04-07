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
    [Trait("Domain", "User - Aggregates")]
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

    [Theory(DisplayName = nameof(InstantiateWithIsActiveStatus))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActiveStatus(bool isActive)
    {
        var validUser = _userTestFixture.GetValidUser();

        var datetimeBefore = DateTime.Now;
        var user = new DomainEntity.User(validUser.Email, validUser.Name, isActive);
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        user.Should().NotBeNull();
        user.Name.Should().Be(validUser.Name);
        user.Email.Should().Be(validUser.Email);
        user.Id.Should().NotBeEmpty();
        user.CreatedAt.Should().NotBeSameDateAs(default);

        (user.CreatedAt >= datetimeBefore).Should().BeTrue();
        (user.CreatedAt <= datetimeAfter).Should().BeTrue();

        user.IsActive.Should().Be(isActive);
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

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "User - Aggregates")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateErrorWhenNameIsEmpty(string name)
    {
        var validUser = _userTestFixture.GetValidUser();

        Action action = () => new DomainEntity.User(validUser.Email, name!);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsMoreThan255Characters))]
    [Trait("Domain", "User - Aggregates")]
    public void InstantiateErrorWhenNameIsMoreThan255Characters()
    {
        var validUser = _userTestFixture.GetValidUser();

        var invalidName = string.Join(
            null,
            Enumerable.Range(1, 256).Select(_ => "a").ToArray()
        );

        Action action = () => new DomainEntity.User(validUser.Email, invalidName);
        action.Should().Throw<EntityValidationException>().WithMessage("Name should not be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "User - Aggregates")]
    public void Activate()
    {
        var validUser = _userTestFixture.GetValidUser();

        var user = new DomainEntity.User(validUser.Email, validUser.Name, false);
        user.Activate();

        user.IsActive.Should().BeTrue();

    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "User - Aggregates")]
    public void Deactivate()
    {
        var validUser = _userTestFixture.GetValidUser();

        var user = new DomainEntity.User(validUser.Email, validUser.Name, true);
        user.Deactivate();

        user.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "User - Aggregates")]
    public void Update()
    {
        var validUser = _userTestFixture.GetValidUser();

        var user = new DomainEntity.User(validUser.Email, validUser.Name);

        var userWithNewValues = _userTestFixture.GetValidUser();

        user.Update(userWithNewValues.Email, userWithNewValues.Name);

        userWithNewValues.Name.Should().Be(user.Name);
        userWithNewValues.Email.Should().Be(user.Email);
    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "User - Aggregates")]
    public void UpdateOnlyName()
    {
        var validUser = _userTestFixture.GetValidUser();

        var user = new DomainEntity.User(validUser.Email, validUser.Name);
        var newName = _userTestFixture.GetValidName();
        var currentEmail = validUser.Email;

        user.Update(name: newName);

        user.Name.Should().Be(newName);
        user.Email.Should().Be(currentEmail);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "User - Aggregates")]
    [InlineData("")]
    [InlineData("  ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var validUser = _userTestFixture.GetValidUser();

        var user = new DomainEntity.User(validUser.Email, validUser.Name);
        Action action = () => user.Update(name: name);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGraterThan255Characters))]
    [Trait("Domain", "User - Aggregates")]
    public void UpdateErrorWhenNameIsGraterThan255Characters()
    {
        var validUser = _userTestFixture.GetValidUser();

        var user = new DomainEntity.User(validUser.Email, validUser.Name);
        var invalidName = _userTestFixture.Faker.Lorem.Letter(256);

        Action action = () => user.Update(name: invalidName);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be greater than 255 characters long");
    }
}
