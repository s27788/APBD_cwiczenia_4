using KlinikaAPI.Data;
using KlinikaAPI.Modele.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KlinikaAPI.Kontrolery;

[ApiController]
[Route("api/[controller]")]
public class WizytaController : ControllerBase
{
    private readonly KlinikaDbContext _context;

    public WizytaController(KlinikaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<WizytaDTO>> GetWizyty()
    {
        var wizyty = _context.Wizyty
            .Select(w => new WizytaDTO
            {
                Id = w.Id,
                Data = w.Data,
                Opis = w.Opis
            })
            .ToList();

        return wizyty;
    }
}