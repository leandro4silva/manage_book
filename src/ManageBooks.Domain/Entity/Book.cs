using ManageBooks.Domain.Enums;
using ManageBooks.Domain.SeedWork;
using ManageBooks.Domain.Validation;

namespace ManageBooks.Domain.Entity;

public class Book : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ISBN { get; private set; }
    public string Author { get; private set; }
    public string PublishingCompany { get; private set; }
    public BookGenre BookGenre { get; private set; }
    public int YearOfPublication { get; private set; }
    public int NumberOfPages { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public decimal AverageGrade { get; private set; }
    public List<Assessment> Assessment { get; private set; }


    public Book(
        string title, 
        string description, 
        string iSBN, 
        string author, 
        string publishingCompany, 
        BookGenre bookGenre, 
        int yearOfPublication, 
        int numberOfPages, 
        decimal averageGrade
    )
    {
        Title = title;
        Description = description;
        ISBN = iSBN;
        Author = author;
        PublishingCompany = publishingCompany;
        BookGenre = bookGenre;
        YearOfPublication = yearOfPublication;
        NumberOfPages = numberOfPages;
        CreatedAt = DateTime.Now;
        AverageGrade = averageGrade;
        Assessment = new List<Assessment>();

        Validate();
    }

    public void Update(
        string? title = null,
        string? description = null,
        string? isbn = null,
        string? author = null,
        string? publishingCompany = null,
        BookGenre? bookGenre = null,
        int? yearOfPublication = null,
        int? numberOfPages = null,
        decimal? averageGrade = null
    )
    {
        Title = title ?? Title;
        Description = description ?? Description;
        ISBN = isbn ?? ISBN;
        Author = author ?? Author;
        PublishingCompany = publishingCompany ?? PublishingCompany;
        BookGenre = bookGenre ?? BookGenre;
        YearOfPublication = yearOfPublication ?? YearOfPublication;
        NumberOfPages = numberOfPages ?? NumberOfPages;
        AverageGrade = averageGrade ?? AverageGrade;
    }

    public void Validate()
    {
        DomainValidation.NotNullOrEmpty(Title, nameof(Title));
        DomainValidation.MinLength(Title, 3, nameof(Title));
        DomainValidation.MaxLength(Title, 255, nameof(Title));

        DomainValidation.NotNullOrEmpty(Description, nameof(Description));
        DomainValidation.MaxLength(Description, 10_000, nameof(Description));

        DomainValidation.NotNullOrEmpty(ISBN, nameof(ISBN));

        DomainValidation.NotNullOrEmpty(Author, nameof(Author));
        DomainValidation.MaxLength(Author, 255, nameof(Author));

        DomainValidation.NotNullOrEmpty(PublishingCompany, nameof(PublishingCompany));
        DomainValidation.MaxLength(PublishingCompany, 255, nameof(PublishingCompany));

        DomainValidation.ValidYear(YearOfPublication, nameof(YearOfPublication));

        DomainValidation.MinValue(NumberOfPages, 10, nameof(NumberOfPages));
        DomainValidation.MaxValue(NumberOfPages, 1500, nameof(NumberOfPages));
    }
}
