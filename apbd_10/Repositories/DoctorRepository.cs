using apbd_10.Context;
using Microsoft.EntityFrameworkCore;

namespace apbd_10.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private MedicalContext _medicalContext;

    public DoctorRepository(MedicalContext medicalContext)
    {
        _medicalContext = medicalContext;
    }

    public async Task<bool> DoctorExistsAsync(int idDoctor)
    {
        var patientExists = await _medicalContext.Doctors
            .FirstOrDefaultAsync(p => p.IdDoctor == idDoctor);
        return patientExists != null;
    }
}