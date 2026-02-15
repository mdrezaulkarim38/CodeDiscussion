using CodeDiscussion.Domain.Common;

namespace CodeDiscussion.Domain.Entities;

public class Question : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
    public string? ExternalUrl { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    public ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
}
