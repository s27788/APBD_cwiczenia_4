namespace KlinikaAPI.Modele.DTO;

public class ZwierzeDTO
{
    public int Id { get; set; }
    public string Imie { get; set; } = null!;
    public string Gatunek { get; set; } = null!;
    public int Wiek { get; set; }

    public List<WizytaDTO> Wizyty { get; set; } = new();
}