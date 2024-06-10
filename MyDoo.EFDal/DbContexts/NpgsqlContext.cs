using Microsoft.EntityFrameworkCore;
using MyDoo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = MyDoo.Entities.UserTask;

namespace MyDoo.EFDal.DbContexts;

public sealed class NpgsqlContext : DbContext
{
    public NpgsqlContext(DbContextOptions<NpgsqlContext> options)
      : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        RegistrateUser(modelBuilder);
        RegistrateTask(modelBuilder);
    }

    private void RegistrateUser(ModelBuilder modelBuilder)
    {
        var users = modelBuilder.Entity<User>();

        users.HasKey(u => u.Id);

        users.Property(u => u.Id).IsRequired();
        users.Property(u => u.Email).IsRequired();
        users.Property(u => u.Name).IsRequired();
        users.Property(u => u.Password).IsRequired();
        users.Property(u => u.TGName).IsRequired();
    }

    private void RegistrateTask(ModelBuilder modelBuilder)
    {
        var tasks = modelBuilder.Entity<Task>();

        tasks.HasKey(t => t.Id);

        tasks.Property(t => t.Id).IsRequired();
        tasks.Property(t => t.UserId).IsRequired();
        tasks.Property(t => t.Text).IsRequired();
        tasks.Property(t => t.Date).IsRequired();
        tasks.Property(t => t.Type).IsRequired();
        tasks.Property(t => t.Important).IsRequired();
        tasks.Property(t => t.Complete).IsRequired();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
}
