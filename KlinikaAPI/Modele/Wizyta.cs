namespace KlinikaAPI.Modele;

public class Wizyta
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Opis { get; set; } = null!;

    // FK
    public int ZwierzeId { get; set; }

    public Zwierze Zwierze { get; set; } = null!;
}