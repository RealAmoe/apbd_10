using apbd_10.Models;

namespace apbd_10.Repositories;

public interface IMedicationRepository
{
    public Task<bool> MedicationExistsAsync(MedicamentDto medicamentDto);
}