#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace LoginAndRegistration.Models;

public class RegistrationContext : DbContext
{
  public RegistrationContext(DbContextOptions options) : base(options) { }

  public DbSet<User> Users { get; set; }
}