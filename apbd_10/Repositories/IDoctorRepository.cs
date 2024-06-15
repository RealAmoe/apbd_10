namespace apbd_10.Repositories;

public interface IDoctorRepository
{
    public Task<bool> DoctorExistsAsync(int idDoctor);
}