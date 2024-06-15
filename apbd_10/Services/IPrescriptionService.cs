using apbd_10.Models;

namespace apbd_10.Services;

public interface IPrescriptionService
{
    public Task AddPrescriptionAsync(AssignPrescriptionDto assignPrescriptionDto);
}