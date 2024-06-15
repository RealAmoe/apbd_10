using apbd_10.Context;
using apbd_10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_10.Repositories;

public class MedicationRepository : IMedicationRepository
{
    private MedicalContext _medicalContext;

    public MedicationRepository(MedicalContext medicalContext)
    {
        _medicalContext = medicalContext;
    }

    public async Task<bool> MedicationExistsAsync(MedicamentDto medicamentDtos)
    {
        var medicationExists = await _medicalContext.Medicaments
            .FirstOrDefaultAsync(m => m.IdMedicament == medicamentDtos.IdMedicament);

        return medicationExists != null;
    }
}