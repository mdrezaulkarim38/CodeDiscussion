using CodeDiscussion.Domain.Common;

namespace CodeDiscussion.Domain.Entities;
public class ApplicationUser : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; } 
    public int Reputation { get; set; } = 0;
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}