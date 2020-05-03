using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleApp.Models
{
  public partial class DatabaseContext : DbContext
  {

    // Add Database tables here
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#error Make sure to update the connection strin gto the correct database
        optionsBuilder.UseNpgsql("server=localhost;database=SampleDatabase");
      }
    }
  }
}
