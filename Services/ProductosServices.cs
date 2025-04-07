using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class ProductosService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> Existe(int productoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .AnyAsync(p => p.ProductoId == productoId);
    }

    public async Task<bool> Existe(int productoId, string nombre)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .AnyAsync(p => p.ProductoId != productoId &&
                          p.Nombre!.ToLower() == nombre.ToLower());
    }

    public async Task<bool> Guardar(Productos producto)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            if (!await Existe(producto.ProductoId))
            {
                producto.FechaRegistro = DateTime.Now;
                contexto.Productos.Add(producto);
            }
            else
            {
                contexto.Productos.Update(producto);
            }

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Eliminar(int productoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            // Verificar si tiene ventas o compras asociadas
            var tieneMovimientos = await contexto.DetalleFacturas
                .AnyAsync(d => d.ProductoId == productoId) ||
                await contexto.DetalleFacturaAdmins
                .AnyAsync(d => d.ProductoId == productoId);

            if (tieneMovimientos)
                return false;

            return await contexto.Productos
                .Where(p => p.ProductoId == productoId)
                .ExecuteDeleteAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Productos?> Buscar(int productoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Include(p => p.Categoria)
            .Include(p => p.DetalleFactura)
            .Include(p => p.DetalleFacturaAdmin)
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<List<Productos>> Listar(
        Expression<Func<Productos, bool>>? criterio = null,
        bool incluirRelaciones = false)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var query = contexto.Productos.AsQueryable();

        if (incluirRelaciones)
        {
            query = query
                .Include(p => p.Categoria)
                .Include(p => p.DetalleFactura)
                .Include(p => p.DetalleFacturaAdmin);
        }

        if (criterio != null)
            query = query.Where(criterio);

        return await query.ToListAsync();
    }

    public async Task<List<Productos>> ListarConStockBajo(int nivelAlerta = 3)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(p => p.Stock <= nivelAlerta)
            .Include(p => p.Categoria)
            .OrderBy(p => p.Stock)
            .ToListAsync();
    }

    public async Task<List<Productos>> ListarPorCategoria(int categoriaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(p => p.CategoriaId == categoriaId)
            .Include(p => p.Categoria)
            .ToListAsync();
    }

    public async Task<decimal> CalcularValorTotalInventario()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .SumAsync(p => p.Stock * p.PrecioUnitario);
    }

    public async Task<bool> ActualizarStock(int productoId, int cantidad)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            var producto = await contexto.Productos.FindAsync(productoId);
            if (producto == null) return false;

            producto.Stock += cantidad;
            if (producto.Stock < 0) return false; // Validar no quede negativo

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<(int TotalProductos, int ProductosBajoStock)> ObtenerEstadisticasInventario(int nivelAlerta = 5)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var totalProductos = await contexto.Productos.CountAsync();
        var productosBajoStock = await contexto.Productos
            .Where(p => p.Stock <= nivelAlerta)
            .CountAsync();

        return (totalProductos, productosBajoStock);
    }
}