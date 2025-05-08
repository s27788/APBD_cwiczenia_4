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

    // GET api/zwierze/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ZwierzeDTO>> GetZwierze(int id)
    {
        var z = await _context.Zwierzeta
            .Include(z => z.Wizyty)
            .FirstOrDefaultAsync(z => z.Id == id);

        if (z == null) return NotFound();

        var dto = new ZwierzeDTO
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
        };

        return dto;
    }

    // POST api/zwierze
    [HttpPost]
    public async Task<ActionResult<ZwierzeDTO>> DodajZwierze(Zwierze nowe)
    {
        _context.Zwierzeta.Add(nowe);
        await _context.SaveChangesAsync();

        var dto = new ZwierzeDTO
        {
            Id = nowe.Id,
            Imie = nowe.Imie,
            Gatunek = nowe.Gatunek,
            Wiek = nowe.Wiek,
            Wizyty = nowe.Wizyty?.Select(w => new WizytaDTO
            {
                Id = w.Id,
                Data = w.Data,
                Opis = w.Opis
            }).ToList() ?? new()
        };

        return CreatedAtAction(nameof(GetZwierze), new { id = nowe.Id }, dto);
    }

    // PUT api/zwierze/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> AktualizujZwierze(int id, Zwierze zaktualizowane)
    {
        if (id != zaktualizowane.Id) return BadRequest();

        _context.Entry(zaktualizowane).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Zwierzeta.Any(z => z.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE api/zwierze/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> UsunZwierze(int id)
    {
        var zwierze = await _context.Zwierzeta
            .Include(z => z.Wizyty)
            .FirstOrDefaultAsync(z => z.Id == id);

        if (zwierze == null) return NotFound();

        _context.Wizyty.RemoveRange(zwierze.Wizyty);
        _context.Zwierzeta.Remove(zwierze);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // GET api/zwierze/statystyki
    [HttpGet("statystyki")]
    public async Task<ActionResult<object>> GetStatystyki()
    {
        var liczbaZwierzat = await _context.Zwierzeta.CountAsync();
        var sredniWiek = await _context.Zwierzeta.AverageAsync(z => z.Wiek);
        var liczbaWizyt = await _context.Wizyty.CountAsync();

        return Ok(new
        {
            LiczbaZwierzat = liczbaZwierzat,
            SredniWiek = Math.Round(sredniWiek, 2),
            LiczbaWizyt = liczbaWizyt
        });
    }

}
