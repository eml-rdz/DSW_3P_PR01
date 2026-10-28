using System.Text.Json;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Repositories;

public sealed class MisionJsonRepository : IMisionRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly string _filePath;
    private readonly SemaphoreSlim _gate = new(1, 1);

    public MisionJsonRepository(string filePath)
    {
        _filePath = filePath;
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
    }

    public async Task<IReadOnlyList<Mision>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try { return await LeerAsync(cancellationToken); }
        finally { _gate.Release(); }
    }

    public async Task<Mision?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try { return (await LeerAsync(cancellationToken)).FirstOrDefault(m => m.Id == id); }
        finally { _gate.Release(); }
    }

    public async Task<Mision> AgregarAsync(Mision mision, CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            var misiones = await LeerAsync(cancellationToken);
            mision.Id = misiones.Count == 0 ? 1 : misiones.Max(m => m.Id) + 1;
            misiones.Add(mision);
            await GuardarAsync(misiones, cancellationToken);
            return mision;
        }
        finally { _gate.Release(); }
    }

    public async Task<bool> ActualizarAsync(Mision mision, CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            var misiones = await LeerAsync(cancellationToken);
            var indice = misiones.FindIndex(m => m.Id == mision.Id);
            if (indice < 0) return false;
            misiones[indice] = mision;
            await GuardarAsync(misiones, cancellationToken);
            return true;
        }
        finally { _gate.Release(); }
    }

    public async Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            var misiones = await LeerAsync(cancellationToken);
            var eliminadas = misiones.RemoveAll(m => m.Id == id);
            if (eliminadas == 0) return false;
            await GuardarAsync(misiones, cancellationToken);
            return true;
        }
        finally { _gate.Release(); }
    }

    private async Task<List<Mision>> LeerAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_filePath)) return [];
        await using var stream = File.OpenRead(_filePath);
        return await JsonSerializer.DeserializeAsync<List<Mision>>(stream, JsonOptions, cancellationToken) ?? [];
    }

    private async Task GuardarAsync(List<Mision> misiones, CancellationToken cancellationToken)
    {
        var temporal = _filePath + ".tmp";
        await using (var stream = File.Create(temporal))
            await JsonSerializer.SerializeAsync(stream, misiones, JsonOptions, cancellationToken);
        File.Move(temporal, _filePath, overwrite: true);
    }
}
