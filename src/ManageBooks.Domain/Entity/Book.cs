﻿using ManageBooks.Domain.Enums;
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


    public void Validate()
    {
        DomainValidation.NotNull(Title, nameof(Title));
        DomainValidation.MinLength(Title, 3, nameof(Title));
        DomainValidation.MaxLength(Title, 255, nameof(Title));

        DomainValidation.NotNull(Description, nameof(Description));
        DomainValidation.MaxLength(Description, 10_000, nameof(Description));

        DomainValidation.NotNull(ISBN, nameof(ISBN));
        DomainValidation.IsUnique(ISBN, nameof(ISBN));

        DomainValidation.NotNull(Author, nameof(Author));
        DomainValidation.MaxLength(Author, 255, nameof(Author));

        DomainValidation.NotNull(PublishingCompany, nameof(PublishingCompany));
        DomainValidation.MaxLength(PublishingCompany, 255, nameof(PublishingCompany));

        DomainValidation.NotNull(BookGenre, nameof(BookGenre));

        DomainValidation.NotNull(YearOfPublication, nameof(YearOfPublication));
        DomainValidation.ValidYear(YearOfPublication, nameof(YearOfPublication));

        DomainValidation.NotNull(NumberOfPages, nameof(NumberOfPages));
        DomainValidation.MinValue(NumberOfPages, 10, nameof(NumberOfPages));
        DomainValidation.MaxValue(NumberOfPages, 1500, nameof(NumberOfPages));
    }
}
