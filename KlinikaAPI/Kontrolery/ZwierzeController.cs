using KlinikaAPI.Data;
using KlinikaAPI.Modele;
using Microsoft.AspNetCore.Mvc;

namespace KlinikaAPI.Kontrolery;

[ApiController]
[Route("api/[controller]")]
public class ZwierzeController : ControllerBase
{
    private readonly KlinikaDbContext _context;

    public ZwierzeController(KlinikaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Zwierze>> GetZwierzeta()
    {
        return _context.Zwierzeta.ToList();
    }

    [HttpPost]
    public ActionResult<Zwierze> DodajZwierze(Zwierze nowe)
    {
        _context.Zwierzeta.Add(nowe);
        _context.SaveChanges();
        return CreatedAtAction(nameof(DodajZwierze), new { id = nowe.Id }, nowe);
    }
}