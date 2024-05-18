using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace new_back.Db;

public partial class PracownicyDbContext : DbContext
{
    public PracownicyDbContext()
    {
    }

    public PracownicyDbContext(DbContextOptions<PracownicyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pracownicy> Pracownicies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:obecni-clone-uwm-project.database.windows.net,1433; Database=Obecni_clone; User ID=michal; Password=Obecni123; Initial Catalog=Obecni_clone; Integrated Security=true; Trusted_Connection=False; Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pracownicy>(entity =>
        {
            entity.ToTable("Pracownicy");
        });
        modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
