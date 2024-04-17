using ManageBooks.Domain.Validation;

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
        Book book
    )
    {
        Note = note;
        Description = description;
        UserId = userId;
        User = user;
        BookId = bookId;
        Book = book;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Update(
        int? note = null,
        string? description = null
    )
    {
        Note = note ?? Note;
        Description = description ?? Description;
    }

    public void Validate()
    {
        DomainValidation.MinValue(Note, 1, nameof(Note));
        DomainValidation.MaxValue(Note, 5, nameof(Note));
        DomainValidation.NotNullOrEmpty(Description, nameof(Description));
    }
}
