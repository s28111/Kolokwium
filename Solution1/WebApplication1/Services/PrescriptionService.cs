using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    public IEnumerable<Prescription_medicament> GetAllPrescription(int idMedicament);
    public bool DeletePatient(int idPatient);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    
    public IEnumerable<Prescription_medicament> GetAllPrescription(int idMedicament)
    {
        return _prescriptionRepository.FetchAllMedicaments(idMedicament);
    }

    public bool DeletePatient(int idPatient)
    {
        return _prescriptionRepository.DeletePatient(idPatient);
    }
}