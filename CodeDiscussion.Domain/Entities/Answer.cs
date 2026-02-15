using CodeDiscussion.Domain.Common;

namespace CodeDiscussion.Domain.Entities;

public class Answer : BaseEntity
{
    public string Content { get; set; } = string.Empty;

    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public Guid UserId { get; set; }
    public int VoteCount { get; set; } = 0;
    public bool IsAccepted { get; set; } = false;
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
