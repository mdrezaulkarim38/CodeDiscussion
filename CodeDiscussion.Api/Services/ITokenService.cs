using CodeDiscussion.Infrastructure.Identity;

public interface ITokenService
{
    string GenerateToken(ApplicationUserIdentity user, IList<string> roles);
}
