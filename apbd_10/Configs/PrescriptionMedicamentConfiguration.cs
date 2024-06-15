using apbd_10.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_10.Configs;

public class PrescriptionMedicamentConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> modelBuilder)
    {
        modelBuilder
            .HasKey(pm => new {pm.IdMedicament, pm.IdPrescription});
        modelBuilder
            .Property(pm => pm.IdMedicament)
            .IsRequired();
        modelBuilder
            .Property(pm => pm.IdPrescription)
            .IsRequired();
        modelBuilder
            .Property(pm => pm.Dose)
            .IsRequired();
        modelBuilder
            .Property(pm => pm.Details)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .ToTable("PrescriptionMedicament");

        modelBuilder
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);
        
        modelBuilder
            .HasOne(pm => pm.Prescription)
            .WithMany(pr => pr.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);
    }
}