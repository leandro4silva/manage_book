using FluentAssertions;
using ManageBooks.Domain.Exceptions;
using DomainEntity = ManageBooks.Domain.Entity;

namespace ManageBooks.UnitTests.Domain.Entity.Assessment;

public class AssessmentTest
{
    private readonly AssessmentTestFixture _assessmentTestFixture;

    public AssessmentTest()
    {
        _assessmentTestFixture = new AssessmentTestFixture();
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Assessment - Aggregates")]
    public void Instantiate()
    {
        var validAssessment = _assessmentTestFixture.GetValidAssessment();

        var datetimeBefore = DateTime.Now;
        var assessment = new DomainEntity.Assessment(
            validAssessment.Note,
            validAssessment.Description,
            validAssessment.UserId,
            validAssessment.User,
            validAssessment.BookId,
            validAssessment.Book
        );
        var datetimeAfter = DateTime.Now.AddSeconds(1);


        assessment.Should().NotBeNull();
        assessment.Id.Should().NotBeEmpty();
        assessment.Note.Should().Be(validAssessment.Note);
        assessment.UserId.Should().Be(validAssessment.UserId);
        assessment.User.Should().Be(validAssessment.User);
        assessment.BookId.Should().Be(validAssessment.BookId);
        assessment.Book.Should().Be(validAssessment.Book);

        (assessment.CreatedAt >= datetimeBefore).Should().BeTrue();
        (assessment.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNoteIsLessThen1Value))]
    [Trait("Domain", "Assessment - Aggregates")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-22)]
    public void InstantiateErrorWhenNoteIsLessThen1Value(int invalidNote)
    {
        var validAssessment = _assessmentTestFixture.GetValidAssessment();

        Action action = () => new DomainEntity.Assessment(
            invalidNote,
            validAssessment.Description,
            validAssessment.UserId,
            validAssessment.User,
            validAssessment.BookId,
            validAssessment.Book
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Note should not be less than 1 value");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNoteIsGreaterThen5Value))]
    [Trait("Domain", "Assessment - Aggregates")]
    [InlineData(6)]
    [InlineData(10)]
    [InlineData(21)]
    public void InstantiateErrorWhenNoteIsGreaterThen5Value(int invalidNote)
    {
        var validAssessment = _assessmentTestFixture.GetValidAssessment();

        Action action = () => new DomainEntity.Assessment(
            invalidNote,
            validAssessment.Description,
            validAssessment.UserId,
            validAssessment.User,
            validAssessment.BookId,
            validAssessment.Book
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Note should not be greater than 5 value");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Assessment - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var validAssessment = _assessmentTestFixture.GetValidAssessment();

        Action action = () => new DomainEntity.Assessment(
            validAssessment.Note,
            null!,
            validAssessment.UserId,
            validAssessment.User,
            validAssessment.BookId,
            validAssessment.Book
        );

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should not be empty or null");
    }
}
