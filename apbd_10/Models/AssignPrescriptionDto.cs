using System.ComponentModel.DataAnnotations;

namespace apbd_10.Models;

public class AssignPrescriptionDto
{
    public PatientDto Patient { get; set; }
    public IEnumerable<MedicamentDto> Medicaments { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
}

public class PatientDto
{
    [Required]
    public int IdPatient { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
}

public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}

public class DoctorDto
{
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
}