using apbd_10.Context;
using apbd_10.Entities;
using apbd_10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_10.Repositories;

public class PatientRepository : IPatientRepository
{
    private MedicalContext _medicalContext;

    public PatientRepository(MedicalContext medicalContext)
    {
        _medicalContext = medicalContext;
    }
    public async Task<bool> PatientExistsAsync(int idPatient)
    {
        var patientExists = await _medicalContext.Patients
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);
        return patientExists != null;
    }

    public async Task<int> AddPatientAsync(PatientDto patientDto)
    {
        var patient = new Patient()
        {
            FirstName = patientDto.FirstName, LastName = patientDto.LastName, BirthDate = patientDto.BirthDate
        };
        _medicalContext.Patients.Add(patient);
        var affectCount = await _medicalContext.SaveChangesAsync();

        return affectCount * patient.IdPatient;
    }

    public async Task<GetPatientDto> GetPatientDataAsync(int idPatient)
    {
        var patientData = await _medicalContext.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .ThenInclude(d => d.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pr => pr.Medicament)
            .ThenInclude(m => m.PrescriptionMedicaments)
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new GetPatientDto()
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName, BirthDate = p.BirthDate,
                Prescriptions = p.Prescriptions.OrderBy(pr => pr.DueDate).Select(pr => new GetPrescriptionDto()
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date, DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new GetMedicamentDto()
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Medicament.Description
                    }).ToList(),
                    Doctor = new GetDoctorDto()
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName
                    }
                }).ToList()
            }).FirstOrDefaultAsync();

        return patientData;
    }
}