using System.ComponentModel.DataAnnotations;

namespace Zajezdnia.Models;

public class Zajezdnia_autobusow
{
    public int Id { get; set; }
    [Required , StringLength(100)]
    public string Nazwa { get; set; }
    [Required , StringLength(200)]
    public string Adres { get; set; }
    [Required, StringLength(50)]
    public string Miasto { get; set; }

    public ICollection<Autobus> Autobusy { get; set; } = new List<Autobus>();
}