using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPrescriptionRepository
{
    public IEnumerable<Prescription_medicament> FetchAllMedicaments(int idMedicament);
    public bool DeletePatient(int idPatient);

}

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly IConfiguration _configuration;

    public PrescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Prescription_medicament> FetchAllMedicaments(int idMedicament)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand(
            @"Select Medicament.Name as MedicamentName,Medicament.Description as MedicamentDescription,Medicament.Type as MedicamentType,Prescription_Medicament.Dose, Prescription_Medicament.Details, Prescription.IdPrescription as PrescriptionId, Prescription.Date as Date from Prescription_Medicament Join Prescription on Prescription_Medicament.IdPrescription = Prescription.IdPrescription Join Medicament on Prescription_Medicament.IdPrescription = Medicament.IdMedicament where Prescription_Medicament.IdMedicament = @idMedicament order by Date",connection);
        command.Parameters.AddWithValue("@idMedicament", idMedicament);
        using var reader = command.ExecuteReader();

        var prescriptions = new List<Prescription_medicament>();
        while (reader.Read())
        {
            var prescription = new Prescription_medicament()
            {
                MedicamentName = reader["MedicamentName"].ToString(),
                MedicamentDescription = reader["MedicamentDescription"].ToString(),
                MedicamentType = reader["MedicamentType"].ToString(),
                Dose = reader["Dose"].ToString(),
                Details = reader["Details"].ToString(),
                IdPrescription = (int)reader["IdPrescription"],
                Date = (DateTime)reader["Date"]
            };
            prescriptions.Add(prescription);
        }

        return prescriptions;
    }

    public bool DeletePatient(int idPatient)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command =
            new SqlCommand(
                @"Delete * from Patient where IdPatient = @idPatient; Delete * From Prescription where IdPatient = @idPatient",connection);
        command.Parameters.AddWithValue("@idPatient", idPatient);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows != 0;
    }
}