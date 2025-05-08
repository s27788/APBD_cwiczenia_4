namespace KlinikaAPI.Modele.DTO;

public class WizytaDTO
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Opis { get; set; } = null!;
}