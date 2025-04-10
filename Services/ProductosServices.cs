using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class ProductosService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<ProductosService> _logger;

    public ProductosService(IDbContextFactory<Contexto> dbFactory, ILogger<ProductosService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos.AnyAsync(p => p.ProductoId == productosId);
    }

    public async Task<bool> Existe(int productosId, string nombre)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos
           .AnyAsync(p => p.ProductoId != productosId && p.Nombre.ToLower() == nombre.ToLower());
    }

    private async Task<bool> Insertar(Productos producto)
    {
        _logger.LogInformation("Insertando nuevo producto");
        await using var contexto = await _dbFactory.CreateDbContextAsync();


        contexto.Productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Productos producto)
    {
        _logger.LogInformation("Modificando producto existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Productos producto)
    {
        if (!await Existe(producto.ProductoId))
        {
            return await Insertar(producto);
        }
        else
        {
            return await Modificar(producto);
        }
    }

    public async Task<Productos?> Buscar(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Include(p => p.DetalleFactura)
            .FirstOrDefaultAsync(p => p.ProductoId == productosId);
    }

    public async Task<bool> Eliminar(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(p => p.ProductoId == productosId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Productos>> Listar(Expression<Func<Productos, bool>> criterio)
    {
        _logger.LogInformation("Listando productos");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<List<Productos>> ListarConStockDisponible()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(p => p.Stock > 0)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ActualizarStock(int productosId, int cantidad)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var producto = await contexto.Productos.FindAsync(productosId);

        if (producto == null) return false;

        producto.Stock += cantidad;

        if (producto.Stock < 0)
        {
            _logger.LogWarning($"Intento de dejar stock negativo para el producto {productosId}");
            return false;
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Productos>> BuscarPorNombre(string nombre)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(p => p.Nombre.Contains(nombre))
            .AsNoTracking()
            .ToListAsync();
    }
}
