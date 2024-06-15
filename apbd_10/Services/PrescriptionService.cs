using apbd_10.Exceptions;
using apbd_10.Models;
using apbd_10.Repositories;

namespace apbd_10.Services;

public class PrescriptionService : IPrescriptionService
{
    private IPrescriptionRepository _prescriptionRepository;
    private IMedicationRepository _medicationRepository;
    private IDoctorRepository _doctorRepository;
    private IPrescriptionMedicamentRepository _prescriptionMedicamentRepository;
    private IPatientRepository _patientRepository;
    private IUnitOfWork _unitOfWork;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository, IMedicationRepository medicationRepository,
        IDoctorRepository doctorRepository, IPrescriptionMedicamentRepository prescriptionMedicamentRepository,
        IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _prescriptionRepository = prescriptionRepository;
        _medicationRepository = medicationRepository;
        _doctorRepository = doctorRepository;
        _prescriptionMedicamentRepository = prescriptionMedicamentRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddPrescriptionAsync(AssignPrescriptionDto assignPrescriptionDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            EnsureDoctorExits(await _doctorRepository.DoctorExistsAsync(assignPrescriptionDto.Doctor.IdDoctor));
            foreach (var medicament in assignPrescriptionDto.Medicaments)
            {
                var medicationExists = await _medicationRepository.MedicationExistsAsync(medicament);
                EnsureMedicationExits(medicationExists);
            }

            MedicationCountInLimit(assignPrescriptionDto.Medicaments.Count());

            DueDateCheck(assignPrescriptionDto.Date, assignPrescriptionDto.DueDate);

            int patientId = assignPrescriptionDto.Patient.IdPatient;
            int doctorId = assignPrescriptionDto.Doctor.IdDoctor;
            var checkPatientExist =
                await _patientRepository.PatientExistsAsync(assignPrescriptionDto.Patient.IdPatient);

            if (!checkPatientExist)
            {
                var patient = await _patientRepository.AddPatientAsync(assignPrescriptionDto.Patient);
                patientId = patient;
            }

            var prescriptionId =
                await _prescriptionRepository.AddPrescriptionAsync(assignPrescriptionDto, patientId, doctorId);
            var prescriptionMedicament = 0;
            if (prescriptionId != 0)
            {
                prescriptionMedicament =
                    await _prescriptionMedicamentRepository.AddToPrescriptionMedicamentAsync(
                        assignPrescriptionDto.Medicaments,
                        prescriptionId);
            }

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
        }
    }

    private static void EnsureMedicationExits(bool medicamentsExist)
    {
        if (!medicamentsExist)
        {
            throw new DoesntExistException("Medicament doesnt exist");
        }
    }

    private static void MedicationCountInLimit(int medicamentCount)    
    {
        if (medicamentCount > 10)
        {
            throw new InvalidRequestException("Medication limit exceeded 10");
        }
    }

    private static void DueDateCheck(DateTime Date, DateTime DueDate)
    {
        if (DueDate <= Date)
        {
            throw new InvalidRequestException("Invalid due date");
        }
    }
    private static void EnsureDoctorExits(bool doctorExist)
    {
        if (!doctorExist)
        {
            throw new DoesntExistException("Doctor doesnt exist");
        }
    }
    
}
