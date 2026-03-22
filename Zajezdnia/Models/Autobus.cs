using System.ComponentModel.DataAnnotations;

namespace Zajezdnia.Models;

public class Autobus
{
    public int Id { get; set; }
    [Required, StringLength(50)] public string NumerRejestracyjny { get; set; }
    public string Marka { get; set; }
    public int RokProdukcji { get; set; }
    public int LiczbaMiejsc { get; set; }
    public string Kolor { get; set; }
    public string Typ { get; set; }

    public int ZajezdniaId { get; set; }
    public Zajezdnia_autobusow Zajezdnia { get; set; }
    
    public ICollection<Kierowca> Kierowcy { get; set; } = new List<Kierowca>();
    public ICollection<Kurs> Kursy { get; set; } = new List<Kurs>();
}