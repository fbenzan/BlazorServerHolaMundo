using ERP.Web.Data;
using ERP.Web.Domain.Dto;
using ERP.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ERP.Web.Services;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> Consultar(string filtro);
    Task<bool> Crear(EmpleadoDto request);
    Task<bool> Eliminar(int Id);
    Task<bool> Modificar(EmpleadoDto request);
}

public class EmpleadoService : IEmpleadoService
{
    private readonly AppDbContext _context;

    public EmpleadoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmpleadoDto>> Consultar(string filtro)
    {
        return await _context.Empleados
            .Where(e => e.DatosPersonales.Nombre.Contains(filtro))
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Personaid = e.PersonaId,
                Sueldo = e.Sueldo,
                Puesto = e.Puesto,
                DatosPersonales = new PersonaDto
                {
                    Id = e.DatosPersonales.Id,
                    Nombre = e.DatosPersonales.Nombre,
                    FechaDeNacimiento = e.DatosPersonales.FechaDeNacimiento
                }
            }).ToListAsync();
    }

    public async Task<bool> Crear(EmpleadoDto request)
    {
        var empleado = Empleado.Create(
            request.DatosPersonales.Nombre,
            request.DatosPersonales.FechaDeNacimiento,
            request.Sueldo,
            request.Puesto,
            request.Bono
            );

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Eliminar(int Id)
    {
        var empleado = await _context.Empleados.FindAsync(Id);
        if (empleado == null)
        {
            return false;
        }

        _context.Empleados.Remove(empleado);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Modificar(EmpleadoDto request)
    {
        var empleado = await _context.Empleados.FindAsync(request.Id);
        if (empleado == null)
        {
            return false;
        }

        empleado.DatosPersonales.Nombre = request.DatosPersonales.Nombre;
        empleado.Sueldo = request.Sueldo;
        empleado.Puesto = request.Puesto;
        empleado.DatosPersonales.Nombre = request.DatosPersonales.Nombre;
        empleado.DatosPersonales.FechaDeNacimiento = request.DatosPersonales.FechaDeNacimiento;

        _context.Empleados.Update(empleado);
        await _context.SaveChangesAsync();
        return true;
    }
}
