using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Web.Domain.Dto;



public class EmpleadoDto
{
    [Key]
    public int Id { get; set; }

    public int Personaid { get; set; }
    public decimal Sueldo { get; set; }
    internal string Puesto { get; set; } = null!;
    internal decimal? Bono { get; set; } = 0;
    public PersonaDto DatosPersonales { get; set; } = new PersonaDto();
}
