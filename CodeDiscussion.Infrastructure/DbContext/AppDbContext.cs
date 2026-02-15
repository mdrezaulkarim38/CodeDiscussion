using Microsoft.AspNetCore.Identity;
using CodeDiscussion.Domain.Entities;
using CodeDiscussion.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeDiscussion.Infrastructure.DbContext;

public class AppDbContext : IdentityDbContext<ApplicationUserIdentity, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Vote> Votes => Set<Vote>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<QuestionTag> QuestionTags => Set<QuestionTag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Question - User
        builder.Entity<Question>()
            .HasOne<ApplicationUserIdentity>()
            .WithMany()
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Answer - Question
        builder.Entity<Answer>()
            .HasOne<ApplicationUserIdentity>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Answer - User
        builder.Entity<Answer>()
            .HasOne<ApplicationUserIdentity>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Vote unique constraint
        builder.Entity<Vote>()
            .HasIndex(v => new { v.UserId, v.AnswerId })
            .IsUnique();

        // Many-to-many QuestionTag
        builder.Entity<QuestionTag>()
            .HasKey(qt => new { qt.QuestionId, qt.TagId });

        builder.Entity<QuestionTag>()
            .HasOne(qt => qt.Question)
            .WithMany(q => q.QuestionTags)
            .HasForeignKey(qt => qt.QuestionId);

        builder.Entity<QuestionTag>()
            .HasOne(qt => qt.Tag)
            .WithMany(t => t.QuestionTags)
            .HasForeignKey(qt => qt.TagId);

        // Soft delete global filter
        builder.Entity<Question>()
            .HasQueryFilter(q => !q.IsDeleted);

        builder.Entity<Answer>()
            .HasQueryFilter(a => !a.IsDeleted);
    }
}
