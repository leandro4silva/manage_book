using FluentAssertions;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Domain.Entity.Book;

public class BookTest
{
    private readonly BookTestFixture _bookTestFixture;

    public BookTest()
    {
        _bookTestFixture = new BookTestFixture();
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Book - Aggregates")]
    public void Instantiate()
    {
        var validBook = _bookTestFixture.GetValidBook();

        var datetimeBefore = DateTime.Now;
        var book = new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        book.Should().NotBeNull();
        book.Id.Should().NotBeEmpty();
        book.Title.Should().Be(validBook.Title);
        book.Description.Should().Be(validBook.Description);
        book.ISBN.Should().Be(validBook.ISBN);
        book.Author.Should().Be(validBook.Author);
        book.PublishingCompany.Should().Be(validBook.PublishingCompany);
        book.BookGenre.Should().Be(validBook.BookGenre);
        book.YearOfPublication.Should().Be(validBook.YearOfPublication);
        book.NumberOfPages.Should().Be(validBook.NumberOfPages);
        book.AverageGrade.Should().Be(validBook.AverageGrade);
    }

    

}
