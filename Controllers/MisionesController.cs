using DSW_3P_PR01.Models;
using DSW_3P_PR01.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSW_3P_PR01.Controllers;

[ApiController]
[Route("api/misiones")]
public class MisionesController(IMisionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Mision>>> ObtenerTodas(CancellationToken cancellationToken) =>
        Ok(await service.ObtenerTodasAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Mision>> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var mision = await service.ObtenerPorIdAsync(id, cancellationToken);
        return mision is null ? NotFound() : Ok(mision);
    }

    [HttpPost]
    public async Task<ActionResult<Mision>> Crear(Mision mision, CancellationToken cancellationToken)
    {
        var creada = await service.CrearAsync(mision, cancellationToken);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Mision mision, CancellationToken cancellationToken)
    {
        if (!await service.ActualizarAsync(id, mision, cancellationToken)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken cancellationToken)
    {
        if (!await service.EliminarAsync(id, cancellationToken)) return NotFound();
        return NoContent();
    }
}
