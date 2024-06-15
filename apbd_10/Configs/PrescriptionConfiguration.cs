using apbd_10.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_10.Configs;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> modelBuilder)
    {
        modelBuilder
            .HasKey(pr => pr.IdPrescription);
        modelBuilder
            .Property(pr => pr.IdPrescription)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder
            .Property(pr => pr.Date)
            .IsRequired();
        modelBuilder
            .Property(pr => pr.DueDate)
            .IsRequired();
        modelBuilder
            .ToTable("Prescription");

        modelBuilder
            .HasOne(pr => pr.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(d => d.IdDoctor);

        modelBuilder
            .HasOne(pr => pr.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdPatient);
    }
}