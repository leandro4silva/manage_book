using FluentAssertions;
using ManageBooks.Domain.Entity;
using ManageBooks.Domain.Exceptions;
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

        (book.CreatedAt >= datetimeBefore).Should().BeTrue();
        (book.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenTitleIsNull))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenTitleIsNull()
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            null!,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Title should not be empty or null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenTitleIsLessThen3Characters))]
    [Trait("Domain", "Book - Aggregates")]
    [MemberData(
        nameof(GetTitleWithLessThan3Characters), 
        parameters: 6
    )]
    public void InstantiateErrorWhenTitleIsLessThen3Characters(string invalidTitle)
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            invalidTitle,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
       );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Title should not be less than 3 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenTitleIsGreaterThen255Characters))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenTitleIsGreaterThen255Characters()
    {
        var validBook = _bookTestFixture.GetValidBook();

        var invalidTitle = string.Join(
            null,
            Enumerable.Range(1, 256).Select(_ => "a").ToArray()
        );

        Action action = () => new DomainEntity.Book(
            invalidTitle, 
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Title should not be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            null!,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThen10_000Characters))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThen10_000Characters()
    {
        var validBook = _bookTestFixture.GetValidBook();

        var invalidDescription = string.Join(
            null,
            Enumerable.Range(1, 10_001).Select(_ => "a").ToArray()
        );

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            invalidDescription,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should not be greater than 10000 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenISBNIsNull))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenISBNIsNull()
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            null!,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("ISBN should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenAuthorIsNull))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenAuthorIsNull()
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            null!,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Author should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenAuthorIsGreaterThen255Characters))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenAuthorIsGreaterThen255Characters()
    {
        var validBook = _bookTestFixture.GetValidBook();

        var invalidAuthor = string.Join(
            null,
            Enumerable.Range(1, 256).Select(_ => "a").ToArray()
        );


        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            invalidAuthor,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Author should not be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenPublishingCompanyIsNull))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenPublishingCompanyIsNull()
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            null!,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("PublishingCompany should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenPublishingCompanyIsGreaterThan255Characters))]
    [Trait("Domain", "Book - Aggregates")]
    public void InstantiateErrorWhenPublishingCompanyIsGreaterThan255Characters()
    {
        var validBook = _bookTestFixture.GetValidBook();

        var invalidPublishingCompany = string.Join(
            null,
            Enumerable.Range(1, 256).Select(_ => "a").ToArray()
        );

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            invalidPublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("PublishingCompany should not be greater than 255 characters long");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenYearOfPublicationIsInvalidYear))]
    [Trait("Domain", "Book - Aggregates")]
    [InlineData(1899)]
    [InlineData(1780)]
    [InlineData(2025)]
    [InlineData(2045)]
    public void InstantiateErrorWhenYearOfPublicationIsInvalidYear(int invalidYearOfPublication)
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            invalidYearOfPublication,
            validBook.NumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("YearOfPublication should be a valid year");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNumberOfPagesIsLessThan10Characters))]
    [Trait("Domain", "Book - Aggregates")]
    [MemberData(
        nameof(GetNumberOfPagesWithLessThan10Characters),
        parameters: 6
    )]
    public void InstantiateErrorWhenNumberOfPagesIsLessThan10Characters(int invalidNumberOfPages)
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            invalidNumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("NumberOfPages should not be less than 10 value");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNumberOfPagesIsGreaterThan1500Characters))]
    [Trait("Domain", "Book - Aggregates")]
    [MemberData(
        nameof(GetNumberOfPagesWithGreaterThan1500Characters),
        parameters: 6
    )]
    public void InstantiateErrorWhenNumberOfPagesIsGreaterThan1500Characters(int invalidNumberOfPages)
    {
        var validBook = _bookTestFixture.GetValidBook();

        Action action = () => new DomainEntity.Book(
            validBook.Title,
            validBook.Description,
            validBook.ISBN,
            validBook.Author,
            validBook.PublishingCompany,
            validBook.BookGenre,
            validBook.YearOfPublication,
            invalidNumberOfPages,
            validBook.AverageGrade
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("NumberOfPages should not be greater than 1500 value");
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Book - Aggregates")]
    public void Update()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var bookWithNewValues = _bookTestFixture.GetValidBook();

        book.Update(
            bookWithNewValues.Title,
            bookWithNewValues.Description,
            bookWithNewValues.ISBN,
            bookWithNewValues.Author,
            bookWithNewValues.PublishingCompany,
            bookWithNewValues.BookGenre,
            bookWithNewValues.YearOfPublication,
            bookWithNewValues.NumberOfPages,
            bookWithNewValues.AverageGrade
        );

        bookWithNewValues.Title.Should().Be(book.Title);
        bookWithNewValues.Description.Should().Be(book.Description);
        bookWithNewValues.ISBN.Should().Be(book.ISBN);
        bookWithNewValues.Author.Should().Be(book.Author);
        bookWithNewValues.PublishingCompany.Should().Be(book.PublishingCompany);
        bookWithNewValues.BookGenre.Should().Be(book.BookGenre);
        bookWithNewValues.YearOfPublication.Should().Be(book.YearOfPublication);
        bookWithNewValues.NumberOfPages.Should().Be(book.NumberOfPages);
        bookWithNewValues.AverageGrade.Should().Be(book.AverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyTitle))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyTitle()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var newTitle = _bookTestFixture.GetValidTitle();
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentBookGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            title: newTitle
        );

        book.Title.Should().Be(newTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentBookGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyDescription))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyDescription()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var newDescription = _bookTestFixture.GetValidDescription();
        var currentTitle = validBook.Title;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentBookGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            description: newDescription
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(newDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentBookGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyISBN))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyISBN()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var newISBN = _bookTestFixture.GetValidISBN();
        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentBookGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            isbn: newISBN 
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(newISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentBookGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyAuthor))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyAuthor()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var newAuthor = _bookTestFixture.GetValidAuthor();
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentBookGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            author: newAuthor
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(newAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentBookGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyPublishingCompany))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyPublishingCompany()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var newPublishingCompany = _bookTestFixture.GetValidPublishingCompany();
        var currentBookGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            publishingCompany: newPublishingCompany
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(newPublishingCompany);
        book.BookGenre.Should().Be(currentBookGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyBookGenre))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyBookGenre()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var newBookGenre = _bookTestFixture.GetValidGenre();
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            bookGenre: newBookGenre
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(newBookGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyYearOfPublication))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyYearOfPublication()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentGenre = validBook.BookGenre;
        var newYearOfPublication = _bookTestFixture.GetValidYearOfPublication();
        var currentNumberOfPages = validBook.NumberOfPages;
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
            yearOfPublication: newYearOfPublication
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentGenre);
        book.YearOfPublication.Should().Be(newYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyNumberOfPages))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyNumberOfPages()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var newNumberOfPages = _bookTestFixture.GetValidNumberOfPages();
        var currentAverageGrade = validBook.AverageGrade;

        book.Update(
           numberOfPages: newNumberOfPages
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(newNumberOfPages);
        book.AverageGrade.Should().Be(currentAverageGrade);
    }

    [Fact(DisplayName = nameof(UpdateOnlyAverageGrade))]
    [Trait("Domain", "Book - Aggregates")]
    public void UpdateOnlyAverageGrade()
    {
        var validBook = _bookTestFixture.GetValidBook();

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

        var currentTitle = validBook.Title;
        var currentDescription = validBook.Description;
        var currentISBN = validBook.ISBN;
        var currentAuthor = validBook.Author;
        var currentPublishingCompany = validBook.PublishingCompany;
        var currentGenre = validBook.BookGenre;
        var currentYearOfPublication = validBook.YearOfPublication;
        var currentNumberOfPages = validBook.NumberOfPages;
        var newAverageGrade = _bookTestFixture.GetValidAverageGrade();

        book.Update(
           averageGrade: newAverageGrade
        );

        book.Title.Should().Be(currentTitle);
        book.Description.Should().Be(currentDescription);
        book.ISBN.Should().Be(currentISBN);
        book.Author.Should().Be(currentAuthor);
        book.PublishingCompany.Should().Be(currentPublishingCompany);
        book.BookGenre.Should().Be(currentGenre);
        book.YearOfPublication.Should().Be(currentYearOfPublication);
        book.NumberOfPages.Should().Be(currentNumberOfPages);
        book.AverageGrade.Should().Be(newAverageGrade);
    }

    public static IEnumerable<object[]> GetNumberOfPagesWithLessThan10Characters(int numberOfTests)
    {
        Random random = new Random();

        for (int i = 0; i < numberOfTests; i++)
        {
            yield return new object[] { random.Next(1, 10) };
        }
    }

    public static IEnumerable<object[]> GetNumberOfPagesWithGreaterThan1500Characters(int numberOfTests)
    {
        Random random = new Random();

        for (int i = 0; i < numberOfTests; i++)
        {
            yield return new object[] { random.Next(1501, 2000) };
        }
    }

    public static IEnumerable<object[]> GetTitleWithLessThan3Characters(int numberOfTests)
    {
        var fixture = new BookTestFixture();

        for (int i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return new object[] { fixture.GetValidTitle()[..(isOdd ? 1 : 2)] };
        }
    }
}
