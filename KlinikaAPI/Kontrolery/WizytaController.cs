using KlinikaAPI.Data;
using KlinikaAPI.Modele;
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
    public ActionResult<IEnumerable<Wizyta>> GetWizyty()
    {
        return _context.Wizyty.ToList();
    }
}