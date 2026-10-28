using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Services;

public interface IMisionService
{
    Task<IReadOnlyList<Mision>> ObtenerTodasAsync(CancellationToken cancellationToken = default);
    Task<Mision?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Mision> CrearAsync(Mision mision, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(int id, Mision mision, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
