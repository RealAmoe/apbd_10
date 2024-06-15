using apbd_10.Configs;
using apbd_10.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_10.Context;

public class MedicalContext : DbContext
{
    public MedicalContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}