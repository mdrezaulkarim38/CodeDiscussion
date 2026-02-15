using Microsoft.AspNetCore.Identity;

namespace CodeDiscussion.Infrastructure.Identity;

public class ApplicationUserIdentity : IdentityUser<Guid>
{
    public string? Bio { get; set; }
    public int Reputation { get; set; } = 0;
}
