using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class DetalleFacturasService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> Existe(int detalleId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .AnyAsync(d => d.DetalleFacturaId == detalleId);
    }

    public async Task<bool> Guardar(DetalleFacturas detalle)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            if (!await Existe(detalle.DetalleFacturaId))
            {
                contexto.DetalleFacturas.Add(detalle);
            }
            else
            {
                contexto.DetalleFacturas.Update(detalle);
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
            await contexto.DetalleFacturas
                .Where(d => d.DetalleFacturaId == detalleId)
                .ExecuteDeleteAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<DetalleFacturas?> Buscar(int detalleId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Include(d => d.Productos)
            .Include(d => d.Facturas)
            .FirstOrDefaultAsync(d => d.DetalleFacturaId == detalleId);
    }

    public async Task<List<DetalleFacturas>> ListarPorFactura(int facturaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.FacturaId == facturaId)
            .Include(d => d.Productos)
            .ToListAsync();
    }

    public async Task<List<DetalleFacturas>> ListarPorProducto(int productoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.ProductoId == productoId)
            .Include(d => d.Facturas)
            .ThenInclude(f => f.Usuarios)
            .ToListAsync();
    }

    public async Task<decimal> ObtenerTotalVentasProducto(int productoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.ProductoId == productoId)
            .SumAsync(d => d.Subtotal);
    }

    public async Task<int> ObtenerCantidadVendidaProducto(int productoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.ProductoId == productoId)
            .SumAsync(d => d.Cantidad);
    }
}