using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Web.Domain.Entities;

[Table("Empleados")]
public class Empleado
{
    [Key]
    public int Id { get; set; }
    public int PersonaId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Sueldo { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Bono { get; set; }
    public string Puesto { get; set; } = null!;

    [ForeignKey(nameof(PersonaId))]
    public virtual Persona DatosPersonales { get; set; } = null!;

    public static Empleado Create(
        string nombre,
        DateTime? fechaNacimiento,
        decimal sueldo,
        string puesto,
        decimal? bono = 0)
    => new()
    {
        Sueldo = sueldo,
        Puesto = puesto,
        Bono = bono,
        DatosPersonales = Persona.Create(nombre, fechaNacimiento)
    };
}
