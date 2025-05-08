namespace KlinikaAPI.Modele;

public class Zwierze
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public double Weight { get; set; }
    public string FurColor { get; set; } = null!;
}