using Bogus;
using DomainEntity = ManageBooks.Domain.Entity; 

namespace ManageBooks.UnitTests.Domain.Entity.User;

public class UserTestFixture
{
    public Faker Faker { get; set; }
    
    public UserTestFixture()
    {
        Faker = new Faker("pt_BR");
    }

    public string GetValidName()
    {
        var userName = Faker.Name.FirstName();


        if (userName.Length > 255)
        {
            userName = userName[..255];
        }

        return userName;
    }

    public string GetValidEmail()
    {
        var userEmail = Faker.Internet.Email();

        return userEmail;
    }

    public DomainEntity.User GetValidUser()
    {
        return new DomainEntity.User(
            GetValidEmail(),
            GetValidName()
        );
    }
}
