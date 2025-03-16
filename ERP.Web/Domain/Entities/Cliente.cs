using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ERP.Web.Domain.Entities;

[Table("Clientes")]
public class Cliente
{
    [Key]
    public int Id { get; set; }
    public int PersonaId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal LimiteDeCredito { get; set; }

    [ForeignKey(nameof(PersonaId))]
    public virtual Persona DatosPersonales { get; set; } = null!;

    public static Cliente Create(
        string nombre,
        DateTime? fechaNacimiento,
        decimal limiteDeCredito)
    => new()
    {
        LimiteDeCredito = limiteDeCredito,
        DatosPersonales = 
        Persona.Create(nombre, fechaNacimiento)
    };
}