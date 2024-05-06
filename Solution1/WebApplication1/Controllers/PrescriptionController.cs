using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[ApiController]
[Route("/api/medicaments")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetMedicament([FromRoute] int id)
    {
        var medicaments = _prescriptionService.GetAllPrescription(id);
        return Ok(medicaments);
    }

    [HttpDelete]
    public IActionResult DeletePatient([FromBody] int id)
    {
        var success = _prescriptionService.DeletePatient(id);
        return success ? StatusCode(StatusCodes.Status204NoContent) : NotFound();
    }
}