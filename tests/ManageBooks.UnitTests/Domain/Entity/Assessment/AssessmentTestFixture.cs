using ManageBooks.UnitTests.Common;
using ManageBooks.UnitTests.Domain.Entity.Book;
using ManageBooks.UnitTests.Domain.Entity.User;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Domain.Entity.Assessment;

public class AssessmentTestFixture : BaseFixture
{
    private readonly UserTestFixture _userTestFixture;
    private readonly BookTestFixture _bookTestFixture;

    public AssessmentTestFixture() : base()
    {
        _userTestFixture = new UserTestFixture();
        _bookTestFixture = new BookTestFixture();
    }

    public int GetValidNote()
    {
        Random random = new Random();
        int validNote = random.Next(1, 5);

        return validNote;
    }

    public string GetValidDescription()
    {
        var validDescription = "";

        while (validDescription.Length < 3)
        {
            validDescription = Faker.Commerce.ProductDescription();
        }

        if (validDescription.Length > 255)
        {
            validDescription = validDescription[..255];
        }

        return validDescription;
    }

    public DomainEntity.User GetValidUser()
    {
        return _userTestFixture.GetValidUser();
    }

    public DomainEntity.Book GetValidBook()
    {
        return _bookTestFixture.GetValidBook();
    }

    public DomainEntity.Assessment GetValidAssessment() 
    {
        var user = GetValidUser();
        var book = GetValidBook();


        return new DomainEntity.Assessment(
            GetValidNote(),
            GetValidDescription(),
            user.Id,
            user,
            book.Id,
            book
        );
    }
}
