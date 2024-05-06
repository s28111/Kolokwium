namespace WebApplication1.Models;

public class Prescription_medicament
{
    public string MedicamentName { get; set; }
    public string MedicamentDescription { get; set; }
    public string MedicamentType { get; set; }
    public string Dose { get; set; }
    public string Details { get; set; }
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
}