using System.ComponentModel.DataAnnotations;

namespace Zajezdnia.Models;

public class Kierowca
{
    public int Id { get; set; }
    [Required,  StringLength(50)]
    public string Imie { get; set; }
    [Required,  StringLength(50)]
    public string Nazwisko { get; set; }
    [Required,  StringLength(10)]
    public string NrPrawazJazdy { get; set; }
    public DateTime DataZatrudnienia { get; set; }
    public string NrTelefonu { get; set; }
    
    public ICollection<Autobus> Autobusy { get; set; } = new List<Autobus>();
    public ICollection<Kurs> Kursy { get; set; } = new List<Kurs>();
}