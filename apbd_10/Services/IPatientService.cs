using apbd_10.Models;

namespace apbd_10.Services;

public interface IPatientService
{
    public Task<GetPatientDto> GetPatientDataAsync(int idPatient);
}