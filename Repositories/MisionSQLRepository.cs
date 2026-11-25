using DSW_3P_PR01.Data;
using DSW_3P_PR01.Models;
using Microsoft.EntityFrameworkCore;

namespace DSW_3P_PR01.Repositories;

public class MisionSQLRepository : IMisionRepository
{
    private readonly AppDbContext _context;

    public MisionSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Mision> ObtenerTodas()
    {
        return _context.Misiones.ToList();
    }

    public async Task<IReadOnlyList<Mision>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Misiones.ToListAsync(cancellationToken);
    }

    public Mision? ObtenerPorId(int Id)
    {
        return _context.Misiones.Find(Id);
    }

    public async Task<Mision?> ObtenerPorIdAsync(int Id, CancellationToken cancellationToken = default)
    {
        return await _context.Misiones.FindAsync(new object[] { Id }, cancellationToken).AsTask();
    }

    public Mision Agregar(Mision mision)
    {
        mision.Id = 0;

        _context.Misiones.Add(mision);
        _context.SaveChanges();

        return mision;
    }

    public async Task<Mision> AgregarAsync(Mision mision, CancellationToken cancellationToken = default)
    {
        mision.Id = 0;

        await _context.Misiones.AddAsync(mision, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return mision;
    }

    public bool Actualizar(int Id, Mision mision)
    {
        var actual = _context.Misiones.Find(Id);

        if (actual == null)
            return false;

        actual.Nombre = mision.Nombre;
        actual.Dificultad = mision.Dificultad;
        actual.RecompensaXP = mision.RecompensaXP;
        actual.Completada = mision.Completada;

        _context.SaveChanges();

        return true;
    }

    public async Task<bool> ActualizarAsync(Mision mision, CancellationToken cancellationToken = default)
    {
        var actual = await _context.Misiones.FindAsync(new object[] { mision.Id }, cancellationToken).AsTask();

        if (actual == null)
            return false;

        actual.Nombre = mision.Nombre;
        actual.Dificultad = mision.Dificultad;
        actual.RecompensaXP = mision.RecompensaXP;
        actual.Completada = mision.Completada;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public bool Eliminar(int Id)
    {
        var mision = _context.Misiones.Find(Id);

        if (mision == null)
            return false;

        _context.Misiones.Remove(mision);
        _context.SaveChanges();

        return true;
    }

    public async Task<bool> EliminarAsync(int Id, CancellationToken cancellationToken = default)
    {
        var mision = await _context.Misiones.FindAsync(new object[] { Id }, cancellationToken).AsTask();

        if (mision == null)
            return false;

        _context.Misiones.Remove(mision);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}