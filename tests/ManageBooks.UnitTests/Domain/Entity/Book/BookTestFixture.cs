using Bogus;
using Bogus.Extensions.Brazil;
using ManageBooks.Domain.Enums;
using ManageBooks.UnitTests.Common;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Domain.Entity.Book;

public class BookTestFixture : BaseFixture
{
    public BookTestFixture() : base()
    {
    }

    public string GetValidTitle()
    {
        var validTitle = "";

        while (validTitle.Length < 3)
        {
            validTitle = Faker.Name.FullName();
        }

        if(validTitle.Length > 255)
        {
            validTitle = validTitle[..255];
        }

        return validTitle;
    }

    public string GetValidDescription()
    {
        var validDescription = Faker.Commerce.ProductDescription();

        if(validDescription.Length > 10_000)
        {
            validDescription = validDescription[..10_000];
        }

        return validDescription;
    }

    public string GetValidISBN()
    {
        var validISBN = Faker.Person.Cpf();

        return validISBN;
    }

    public string GetValidAuthor()
    {
        var validAuthor = "";

        while (validAuthor.Length < 3)
        {
            validAuthor = Faker.Name.FullName();
        }

        if (validAuthor.Length > 255)
        {
            validAuthor = validAuthor[..255];
        }

        return validAuthor;
    }

    public string GetValidPublishingCompany()
    {
        var validPublishingCompany = Faker.Company.CompanyName();

        if (validPublishingCompany.Length > 255)
        {
            validPublishingCompany = validPublishingCompany[..255];
        }

        return validPublishingCompany;
    }

    public BookGenre GetValidGenre()
    {
        BookGenre validGenre = BookGenre.Horror;

        Random random = new Random();
        int randomNumber = random.Next(0, 5);
    
        switch (randomNumber)
        {
            case 0:
                validGenre = BookGenre.Horror;
                break;
            case 1:
                validGenre = BookGenre.Scifi;
                break;
            case 2:
                validGenre = BookGenre.Romance;
                break;
            case 3:
                validGenre = BookGenre.Religious;
                break;
            case 4:
                validGenre = BookGenre.Biographies;
                break;
            default: break;
        }

        return validGenre;
    }

    public int GetValidYearOfPublication()
    {
        Random random = new Random();
        var yearPublication = random.Next(1910, DateTime.Now.Year);

        return yearPublication;
    }

    public int GetValidNumberOfPages()
    {
        Random random = new Random();

        var minPages = 10;
        var maxPages = 1501;

        var numberOfPages = random.Next(minPages, maxPages);

        return numberOfPages;
    }

    public decimal GetValidAverageGrade()
    {
        Random random = new Random();

        var minNote = 0;
        var maxNote = 6;

        var averageGrade = random.Next(minNote, maxNote);

        return averageGrade;
    }

    public DomainEntity.Book GetValidBook()
    {
        return new DomainEntity.Book(
            GetValidTitle(),
            GetValidDescription(),
            GetValidISBN(),
            GetValidAuthor(),
            GetValidPublishingCompany(),
            GetValidGenre(),
            GetValidYearOfPublication(),
            GetValidNumberOfPages(),
            GetValidAverageGrade()
        );
    }
}
