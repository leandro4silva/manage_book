﻿using ManageBooks.Domain.Enums;
using ManageBooks.Domain.SeedWork;

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
        DateTime createdAt, 
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
        CreatedAt = createdAt;
        AverageGrade = averageGrade;
        Assessment = new List<Assessment>();
    }

   
}