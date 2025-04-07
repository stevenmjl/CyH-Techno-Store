using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class ProveedoresService(IDbContextFactory<Contexto> dbFactory)
{
    private async Task<bool> Existe(int proveedorId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Proveedores
            .AnyAsync(p => p.ProveedorId == proveedorId);
    }

    public async Task<bool> Existe(int proveedorId, string? nombres, string? rnc)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Proveedores
            .AnyAsync(p => p.ProveedorId != proveedorId &&
                          (p.Nombres!.ToLower() == nombres!.ToLower() ||
                           p.RNC == rnc));
    }

    private async Task<bool> Insertar(Proveedores proveedor)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Proveedores.Add(proveedor);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Proveedores proveedor)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Proveedores.Update(proveedor);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Proveedores proveedor)
    {
        if (!await Existe(proveedor.ProveedorId))
            return await Insertar(proveedor);
        else
            return await Modificar(proveedor);
    }

    public async Task<Proveedores?> Buscar(int proveedorId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Proveedores
            .FirstOrDefaultAsync(p => p.ProveedorId == proveedorId);
    }

    // Método para eliminar un proveedor (con validación de facturas asociadas)
    public async Task<bool> Eliminar(int proveedorId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        // Verificar si tiene facturas asociadas
        var tieneFacturas = await contexto.FacturaAdmins
            .AnyAsync(f => f.ProveedorId == proveedorId);

        if (tieneFacturas)
            return false; // No se puede eliminar si tiene facturas

        return await contexto.Proveedores
            .Where(p => p.ProveedorId == proveedorId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Proveedores>> Listar(Expression<Func<Proveedores, bool>>? criterio = null)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var query = contexto.Proveedores.AsQueryable();

        if (criterio != null)
            query = query.Where(criterio);

        return await query.ToListAsync();
    }

    public async Task<int> UltimoId()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var ultimoProveedor = await contexto.Proveedores
            .OrderByDescending(p => p.ProveedorId)
            .FirstOrDefaultAsync();

        return ultimoProveedor?.ProveedorId ?? 0;
    }
}