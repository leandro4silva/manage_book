using ManageBooks.Application.Interfaces;
using ManageBooks.Domain.Repository;
using ManageBooks.UnitTests.Common;
using Moq;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Application.Common;

public class UserBaseFixture : BaseFixture
{
    public string GetValidUserName()
    {
        var userName = Faker.Name.FirstName();


        if (userName.Length > 255)
        {
            userName = userName[..255];
        }

        return userName;
    }

    public string GetValidUserEmail()
    {
        var userEmail = Faker.Internet.Email();

        return userEmail;
    }

    public DomainEntity.User GetValidUser()
    {
        return new DomainEntity.User(
            GetValidUserEmail(),
            GetValidUserName()
        );
    }

    public Mock<IUserRepository> GetRepositoryMock()
    {
        return new Mock<IUserRepository>();
    }

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
    {
        return new Mock<IUnitOfWork>();
    }
}
