using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models.Entities;
using PhoneBook.Models.Entities.Abstraction;
using PhoneBook.Models.Services.Abstraction;

namespace PhoneBook.DAL.DB
{
  public class PhoneBookContext : IdentityDbContext<SystemUser, Role, int, IdentityUserClaim<int>, UserRole,
      IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
  {
    private readonly ICurrentUserService _currentUserService;
    public PhoneBookContext(DbContextOptions<PhoneBookContext> options, ICurrentUserService currentUserService) : base(options)
    {
      _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

      //base.OnModelCreating(builder);

      var cascadeFKs = builder.Model.GetEntityTypes()
          .SelectMany(t => t.GetForeignKeys())
          .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

      foreach (var fk in cascadeFKs)
        fk.DeleteBehavior = DeleteBehavior.Restrict;

      builder.Entity<SystemUser>().ToTable("Users")
          .HasMany(e => e.Roles)
          .WithMany(e => e.Users)
          .UsingEntity<UserRole>();

      builder.Entity<Role>().ToTable("Roles");

      builder.Entity<UserRole>()
          .ToTable("UserRoles")
          .HasKey(x => new { x.UserId, x.RoleId });

      builder.Entity<IdentityUserClaim<int>>(entity => { entity.ToTable("UserClaims"); });

      builder.Entity<IdentityUserLogin<int>>(entity =>
      {
        entity.ToTable("UserLogins");
        entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });
      });

      builder.Entity<IdentityUserToken<int>>(entity =>
      {
        entity.ToTable("UserTokens");
        entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
      });

      builder.Entity<IdentityRoleClaim<int>>(entity => { entity.ToTable("RoleClaims"); });

      builder.Entity<Contact>()
        .HasIndex(x => x.PhoneNumber)
        .IsUnique();

      SeedData.Seed(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var entries = ChangeTracker.Entries();

      foreach (var entry in entries)
      {
        if (entry.Entity is ISoftDelete softDeleteEntry)
        {
          if (entry.State == EntityState.Added)
          {
            softDeleteEntry.IsActive = true;
          }
          else if (entry.State == EntityState.Deleted)
          {
            entry.State = EntityState.Modified;
            softDeleteEntry.IsActive = false; 
          }
        }

        if (entry.Entity is IAuditable auditableEntry)
        {
          if (entry.State == EntityState.Added)
          {
            auditableEntry.CreatedOn = DateTime.UtcNow;
            auditableEntry.CreatedBy = _currentUserService.GetCurrentUserId(); 
          }

          if (entry.State == EntityState.Modified)
          {
            auditableEntry.UpdatedOn = DateTime.UtcNow;
            auditableEntry.UpdatedBy = _currentUserService.GetCurrentUserId();
          }
        }
      }


      return base.SaveChangesAsync(cancellationToken);
    }


    public DbSet<Contact> Contacts { get; set; }
  }
}
