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
        return await contexto.DetalleFacturas.AnyAsync(d => d.DetalleFacturaId == detalleId);
    }

    public async Task<bool> ExisteProductoEnFactura(int facturasId, int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
           .AnyAsync(d => d.FacturaId == facturasId && d.ProductoId == productosId);
    }

    public async Task<int> Insertar(DetalleFacturas detalle)
    {
        _logger.LogInformation("Insertando nuevo detalle de factura");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
        if (producto == null || producto.Stock < detalle.Cantidad)
        {
            _logger.LogWarning($"No hay suficiente stock para el producto {detalle.ProductoId}");
            return -1; // Usamos -1 para indicar error
        }

        contexto.DetalleFacturas.Add(detalle);
        producto.Stock -= detalle.Cantidad;
        await contexto.SaveChangesAsync();
        return detalle.DetalleFacturaId;
    }

    private async Task<bool> Modificar(DetalleFacturas detalle)
    {
        _logger.LogInformation("Modificando detalle de factura existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        var detalleExistente = await contexto.DetalleFacturas
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DetalleFacturaId == detalle.DetalleFacturaId);

        if (detalleExistente == null) return false;


        var diferenciaCantidad = detalle.Cantidad - detalleExistente.Cantidad;
        var producto = await contexto.Productos.FindAsync(detalle.ProductoId);

        if (producto == null || producto.Stock < diferenciaCantidad)
        {
            _logger.LogWarning($"No hay suficiente stock para actualizar el detalle {detalle.DetalleFacturaId}");
            return false;
        }


        producto.Stock -= diferenciaCantidad;

        contexto.Update(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<int> Guardar(DetalleFacturas detalle)
    {
        if (!await Existe(detalle.DetalleFacturaId))
        {
            return await Insertar(detalle);
        }
        else
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var detalleExistente = await contexto.DetalleFacturas
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DetalleFacturaId == detalle.DetalleFacturaId);

            if (detalleExistente == null) return -1;

            var diferenciaCantidad = detalle.Cantidad - detalleExistente.Cantidad;
            var producto = await contexto.Productos.FindAsync(detalle.ProductoId);

            if (producto == null || producto.Stock < diferenciaCantidad)
            {
                _logger.LogWarning($"No hay suficiente stock para actualizar el detalle {detalle.DetalleFacturaId}");
                return -1;
            }

            producto.Stock -= diferenciaCantidad;
            contexto.Update(detalle);
            await contexto.SaveChangesAsync();
            return detalle.DetalleFacturaId;
        }
    }

    public async Task<DetalleFacturas?> Buscar(int detalleId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Include(d => d.Facturas)
            .Include(d => d.Productos)
            .FirstOrDefaultAsync(d => d.DetalleFacturaId == detalleId);
    }

    public async Task<bool> Eliminar(int detalleId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        var detalle = await contexto.DetalleFacturas.FindAsync(detalleId);
        if (detalle == null) return false;


        var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
        if (producto != null)
        {
            producto.Stock += detalle.Cantidad;
        }

        return await contexto.DetalleFacturas
            .Where(d => d.DetalleFacturaId == detalleId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<DetalleFacturas>> ListarPorFactura(int facturasId)
    {
        _logger.LogInformation($"Listando detalles para factura {facturasId}");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.FacturaId == facturasId)
            .Include(d => d.Productos)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<DetalleFacturas>> ListarPorProducto(int productosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.ProductoId == productosId)
            .Include(d => d.Facturas)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<decimal> ObtenerTotalPorFactura(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.FacturaId == facturasId)
            .SumAsync(d => d.Subtotal);
    }
}
