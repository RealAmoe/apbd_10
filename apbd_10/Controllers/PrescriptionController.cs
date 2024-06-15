using apbd_10.Models;
using apbd_10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apbd_10.Controllers;
[ApiController]
[Authorize]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(AssignPrescriptionDto assignPrescriptionDto)
    {
        await _prescriptionService.AddPrescriptionAsync(assignPrescriptionDto);
        
        return NoContent();
    }
}