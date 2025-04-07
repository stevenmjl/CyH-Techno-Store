using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class TransaccionesService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> RegistrarTransaccion(Transacciones transaccion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            contexto.Transacciones.Add(transaccion);
            return await contexto.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> EliminarTransaccion(int facturaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Transacciones
            .Where(t => t.FacturaId == facturaId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Transacciones>> ListarTransacciones(
        Expression<Func<Transacciones, bool>>? criterio = null)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var query = contexto.Transacciones
            .Include(t => t.Facturas)
            .Include(t => t.FacturaAdmins)
            .AsQueryable();

        if (criterio != null)
            query = query.Where(criterio);

        return await query.ToListAsync();
    }

    public async Task<decimal> ObtenerBalance()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var ingresos = await contexto.Transacciones
            .Where(t => t.Tipo == "Ingreso")
            .SumAsync(t => t.Monto);

        var gastos = await contexto.Transacciones
            .Where(t => t.Tipo == "Gasto")
            .SumAsync(t => t.Monto);

        return ingresos - gastos;
    }
}