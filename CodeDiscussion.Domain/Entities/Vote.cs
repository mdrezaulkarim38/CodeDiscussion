using CodeDiscussion.Domain.Common;
using CodeDiscussion.Domain.Enums;

namespace CodeDiscussion.Domain.Entities;

public class Vote : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public Guid AnswerId { get; set; }
    public Answer Answer { get; set; } = null!;

    public VoteType VoteType { get; set; }
}
