namespace KlinikaAPI.Modele;

public class Wizyta
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}