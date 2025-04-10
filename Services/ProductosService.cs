
using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class ProductossService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<ProductossService> _logger;

    public ProductossService(IDbContextFactory<Contexto> dbFactory, ILogger<ProductossService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss.AnyAsync(p => p.ProductosId == productosId);
    }

    public async Task<bool> Existe(int productosId, string nombre)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss
           .AnyAsync(p => p.ProductosId != productosId && p.Nombre.ToLower() == nombre.ToLower());
    }

    private async Task<bool> Insertar(Productoss producto)
    {
        _logger.LogInformation("Insertando nuevo producto");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

     
        contexto.Productoss.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Productoss producto)
    {
        _logger.LogInformation("Modificando producto existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Productoss producto)
    {
        if (!await Existe(producto.ProductosId))
        {
            return await Insertar(producto);
        }
        else
        {
            return await Modificar(producto);
        }
    }

    public async Task<Productoss?> Buscar(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss
            .Include(p => p.DetallesFacturas) 
            .FirstOrDefaultAsync(p => p.ProductosId == productosId);
    }

    public async Task<bool> Eliminar(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss
            .Where(p => p.ProductosId == productosId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Productoss>> Listar(Expression<Func<Productoss, bool>> criterio)
    {
        _logger.LogInformation("Listando productos");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

 
    public async Task<List<Productoss>> ListarConStockDisponible()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss
            .Where(p => p.Stock > 0)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ActualizarStock(int productosId, int cantidad)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var producto = await contexto.Productoss.FindAsync(productosId);

        if (producto == null) return false;

        producto.Stock += cantidad;

        if (producto.Stock < 0) 
        {
            _logger.LogWarning($"Intento de dejar stock negativo para el producto {productosId}");
            return false;
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Productoss>> BuscarPorNombre(string nombre)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Productoss
            .Where(p => p.Nombre.Contains(nombre))
            .AsNoTracking()
            .ToListAsync();
    }
}