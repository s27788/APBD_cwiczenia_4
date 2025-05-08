using KlinikaAPI.Data;
using KlinikaAPI.Modele;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    // GET api/zwierze
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Zwierze>>> GetZwierzeta()
    {
        return await _context.Zwierzeta
            .Include(z => z.Wizyty) // includes visits
            .ToListAsync();
    }

    // POST api/zwierze
    [HttpPost]
    public async Task<ActionResult<Zwierze>> DodajZwierze(Zwierze nowe)
    {
        _context.Zwierzeta.Add(nowe);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetZwierzeta), new { id = nowe.Id }, nowe);
    }
}