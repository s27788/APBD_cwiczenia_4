namespace KlinikaAPI.Modele;

public class Zwierze
{
    public int Id { get; set; }
    public string Imie { get; set; }
    public string Gatunek { get; set; }
    public int Wiek { get; set; }

    public List<Wizyta> Wizyty { get; set; }
}