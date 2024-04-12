using FluentAssertions;
using ManageBooks.Domain.Entity;
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
}
