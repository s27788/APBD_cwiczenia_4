using KlinikaAPI.Data;
using KlinikaAPI.Modele;
using KlinikaAPI.Modele.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet("{id}")]
    public ActionResult<WizytaDTO> GetWizyta(int id)
    {
        var wizyta = _context.Wizyty.Find(id);
        if (wizyta == null) return NotFound();

        return new WizytaDTO
        {
            Id = wizyta.Id,
            Data = wizyta.Data,
            Opis = wizyta.Opis
        };
    }

    [HttpPost]
    public ActionResult<Wizyta> DodajWizyte(Wizyta wizyta)
    {
        _context.Wizyty.Add(wizyta);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetWizyta), new { id = wizyta.Id }, wizyta);
    }

    [HttpPut("{id}")]
    public IActionResult AktualizujWizyte(int id, Wizyta nowa)
    {
        if (id != nowa.Id) return BadRequest();

        var istnieje = _context.Wizyty.Any(w => w.Id == id);
        if (!istnieje) return NotFound();

        _context.Entry(nowa).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult UsunWizyte(int id)
    {
        var wizyta = _context.Wizyty.Find(id);
        if (wizyta == null) return NotFound();

        _context.Wizyty.Remove(wizyta);
        _context.SaveChanges();

        return NoContent();
    }
}