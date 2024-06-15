using apbd_10.Context;
using apbd_10.Entities;
using apbd_10.Models;

namespace apbd_10.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private MedicalContext _medicalContext;

    public PrescriptionRepository(MedicalContext medicalContext)
    {
        _medicalContext = medicalContext;
    }

    public async Task<int> AddPrescriptionAsync(AssignPrescriptionDto assignPrescriptionDto, int doctorId,
        int patientId)
    {
        var prescription = new Prescription()
        {
            Date = assignPrescriptionDto.Date, DueDate = assignPrescriptionDto.DueDate, IdPatient = doctorId,
            IdDoctor = patientId
        };
        _medicalContext.Prescriptions.Add(prescription);
        var affectedCount = await _medicalContext.SaveChangesAsync();

        return affectedCount * prescription.IdPrescription;
    }
    
}