using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class DetalleFacturasService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<DetalleFacturasService> _logger;

    public DetalleFacturasService(IDbContextFactory<Contexto> dbFactory, ILogger<DetalleFacturasService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int detalleId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetallesFacturas.AnyAsync(d => d.DetalleId == detalleId);
    }

    public async Task<bool> ExisteProductoEnFactura(int facturasId, int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetallesFacturas
           .AnyAsync(d => d.FacturasId == facturasId && d.ProductosId == productosId);
    }

    private async Task<bool> Insertar(DetalleFacturass detalle)
    {
        _logger.LogInformation("Insertando nuevo detalle de factura");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        
    
        var producto = await contexto.Productoss.FindAsync(detalle.ProductosId);
        if (producto == null || producto.Stock < detalle.Cantidad)
        {
            _logger.LogWarning($"No hay suficiente stock para el producto {detalle.ProductosId}");
            return false;
        }
        
        contexto.DetallesFacturas.Add(detalle);
        
      
        producto.Stock -= detalle.Cantidad;
        
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(DetalleFacturass detalle)
    {
        _logger.LogInformation("Modificando detalle de factura existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        
        var detalleExistente = await contexto.DetallesFacturas
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DetalleId == detalle.DetalleId);
            
        if (detalleExistente == null) return false;
        
       
        var diferenciaCantidad = detalle.Cantidad - detalleExistente.Cantidad;
        var producto = await contexto.Productoss.FindAsync(detalle.ProductosId);
        
        if (producto == null || producto.Stock < diferenciaCantidad)
        {
            _logger.LogWarning($"No hay suficiente stock para actualizar el detalle {detalle.DetalleId}");
            return false;
        }
        
       
        producto.Stock -= diferenciaCantidad;
        
        contexto.Update(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(DetalleFacturass detalle)
    {
        if (!await Existe(detalle.DetalleId))
        {
            return await Insertar(detalle);
        }
        else
        {
            return await Modificar(detalle);
        }
    }

    public async Task<DetalleFacturass?> Buscar(int detalleId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetallesFacturas
            .Include(d => d.Facturas)
            .Include(d => d.Productos)
            .FirstOrDefaultAsync(d => d.DetalleId == detalleId);
    }

    public async Task<bool> Eliminar(int detalleId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        
        var detalle = await contexto.DetallesFacturas.FindAsync(detalleId);
        if (detalle == null) return false;
        
     
        var producto = await contexto.Productoss.FindAsync(detalle.ProductosId);
        if (producto != null)
        {
            producto.Stock += detalle.Cantidad;
        }
        
        return await contexto.DetallesFacturas
            .Where(d => d.DetalleId == detalleId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<DetalleFacturass>> ListarPorFactura(int facturasId)
    {
        _logger.LogInformation($"Listando detalles para factura {facturasId}");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetallesFacturas
            .Where(d => d.FacturasId == facturasId)
            .Include(d => d.Productos)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<DetalleFacturass>> ListarPorProducto(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetallesFacturas
            .Where(d => d.ProductosId == productosId)
            .Include(d => d.Facturas)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<decimal> ObtenerTotalPorFactura(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetallesFacturas
            .Where(d => d.FacturasId == facturasId)
            .SumAsync(d => d.Subtotal);
    }
}