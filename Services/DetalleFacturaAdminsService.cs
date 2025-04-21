using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class DetalleFacturaAdminsService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> Existe(int detalleId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturaAdmins
            .AnyAsync(d => d.DetalleFacturaAdminId == detalleId);
    }

    public async Task<bool> Guardar(DetalleFacturaAdmins detalle)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            if (!await Existe(detalle.DetalleFacturaAdminId))
            {
                contexto.DetalleFacturaAdmins.Add(detalle);
            }
            else
            {
                contexto.DetalleFacturaAdmins.Update(detalle);
            }

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Eliminar(int detalleId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            await contexto.DetalleFacturaAdmins
                .Where(d => d.DetalleFacturaAdminId == detalleId)
                .ExecuteDeleteAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<DetalleFacturaAdmins?> Buscar(int detalleId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturaAdmins
            .Include(d => d.Productos)
            .Include(d => d.FacturaAdmins)
            .FirstOrDefaultAsync(d => d.DetalleFacturaAdminId == detalleId);
    }

    public async Task<List<DetalleFacturaAdmins>> ListarTodo()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturaAdmins
            .Include(d => d.Productos)
            .Include(d => d.FacturaAdmins)
            .ToListAsync();
    }
}