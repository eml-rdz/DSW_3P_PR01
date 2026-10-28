using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public sealed class MisionService(IMisionRepository repository) : IMisionService
{
    public Task<IReadOnlyList<Mision>> ObtenerTodasAsync(CancellationToken cancellationToken = default) =>
        repository.ObtenerTodasAsync(cancellationToken);

    public Task<Mision?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        repository.ObtenerPorIdAsync(id, cancellationToken);

    public Task<Mision> CrearAsync(Mision mision, CancellationToken cancellationToken = default)
    {
        mision.Id = 0;
        mision.Completada = false;
        return repository.AgregarAsync(mision, cancellationToken);
    }

    public Task<bool> ActualizarAsync(int id, Mision mision, CancellationToken cancellationToken = default)
    {
        mision.Id = id;
        return repository.ActualizarAsync(mision, cancellationToken);
    }

    public Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default) =>
        repository.EliminarAsync(id, cancellationToken);
}
