using System.ComponentModel.DataAnnotations;

namespace Zajezdnia.Models;

public class Kurs
{
    public int Id { get; set; }
    public DateTime DataOdjazdu { get; set; }
    [Required, StringLength(20)]
    public string NumerLinii { get; set; }
    [Required, StringLength(50)]
    public string Trasa { get; set; }

    public int KierowcaId { get; set; }
    public Kierowca Kierowca { get; set; }

    public int AutobusId { get; set; }
    public Autobus Autobus { get; set; }
}