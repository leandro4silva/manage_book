﻿using SeedWork = ManageBooks.Domain.SeedWork;

namespace ManageBooks.Domain.Entity;

public class Assessment : SeedWork.Entity
{
    public int Note { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public Guid BookId { get; private set; }
    public Book Book { get; private set; }
    public DateTime CreatedAt { get; private set; }


    public Assessment(
        int note, 
        string description, 
        Guid userId, 
        User user, 
        Guid bookId, 
        Book book, 
        DateTime createdAt
    )
    {
        Note = note;
        Description = description;
        UserId = userId;
        User = user;
        BookId = bookId;
        Book = book;
        CreatedAt = createdAt;
    }
}