using KlinikaAPI.Data;
using KlinikaAPI.Modele;
using KlinikaAPI.Modele.DTO;
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
    public async Task<ActionResult<IEnumerable<ZwierzeDTO>>> GetZwierzeta()
    {
        var zwierzeta = await _context.Zwierzeta
            .Include(z => z.Wizyty)
            .Select(z => new ZwierzeDTO
            {
                Id = z.Id,
                Imie = z.Imie,
                Gatunek = z.Gatunek,
                Wiek = z.Wiek,
                Wizyty = z.Wizyty.Select(w => new WizytaDTO
                {
                    Id = w.Id,
                    Data = w.Data,
                    Opis = w.Opis
                }).ToList()
            })
            .ToListAsync();

        return zwierzeta;
    }

    // POST api/zwierze
    [HttpPost]
    public async Task<ActionResult<Zwierze>> DodajZwierze([FromBody] Zwierze nowe)
    {
        _context.Zwierzeta.Add(nowe);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetZwierzeta), new { id = nowe.Id }, nowe);
    }
}