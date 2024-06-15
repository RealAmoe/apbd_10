using apbd_10.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_10.Configs;

public class DoctorConfiguration :IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> modelBuilder)
    {
        modelBuilder
            .HasKey(p => p.IdDoctor);
        modelBuilder
            .Property(p => p.IdDoctor)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder
            .Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .Property(p => p.LastName)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .Property(p => p.Email)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .ToTable("Doctor");
    }
}