using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Repositories;

public interface IMisionRepository
{
    Task<IReadOnlyList<Mision>> ObtenerTodasAsync(CancellationToken cancellationToken = default);
    Task<Mision?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Mision> AgregarAsync(Mision mision, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(Mision mision, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
