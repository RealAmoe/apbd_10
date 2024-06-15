using apbd_10.Exceptions;
using apbd_10.Models;
using apbd_10.Repositories;

namespace apbd_10.Services;

public class PatientService : IPatientService
{
    private IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<GetPatientDto> GetPatientDataAsync(int idPatient)
    {
        var patient = await _patientRepository.PatientExistsAsync(idPatient);
        if (!patient)
        {
            throw new DoesntExistException("Patient doesnt exist");
        }
        return await _patientRepository.GetPatientDataAsync(idPatient);
    }
}