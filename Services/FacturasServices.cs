using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class FacturasService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<FacturasService> _logger;

    public FacturasService(IDbContextFactory<Contexto> dbFactory, ILogger<FacturasService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturas.AnyAsync(f => f.FacturaId == facturasId);
    }

    public async Task<bool> Existe(DateTime fecha, int usuarioId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
           .AnyAsync(f => f.FechaRegistro.Date == fecha.Date && f.UsuarioId == usuarioId);
    }

    public async Task<int> Insertar(Facturas factura)
    {
        _logger.LogInformation("Insertando nueva factura");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        contexto.Facturas.Add(factura);
        await contexto.SaveChangesAsync();
        return factura.FacturaId;
    }

    private async Task<bool> Modificar(Facturas factura)
    {
        _logger.LogInformation("Modificando factura existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        contexto.Update(factura);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<int> Guardar(Facturas factura)
    {
        if (!await Existe(factura.FacturaId))
        {
            return await Insertar(factura);
        }
        else
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Update(factura);
            await contexto.SaveChangesAsync();
            return factura.FacturaId;
        }
    }

    public async Task<Facturas?> Buscar(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Include(f => f.Usuarios)
            .Include(f => f.DetalleFacturas)
                .ThenInclude(d => d.Productos)
            .FirstOrDefaultAsync(f => f.FacturaId == facturasId);
    }

    public async Task<bool> Eliminar(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        await contexto.DetalleFacturas
            .Where(d => d.FacturaId == facturasId)
            .ExecuteDeleteAsync();


        return await contexto.Facturas
            .Where(f => f.FacturaId == facturasId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Facturas>> Listar(Expression<Func<Facturas, bool>> criterio)
    {
        _logger.LogInformation("Listando facturas");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Include(f => f.Usuarios)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<List<Facturas>> ListarPorUsuario(int usuarioId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Where(f => f.UsuarioId == usuarioId)
            .Include(f => f.DetalleFacturas)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Facturas>> ListarPorFecha(DateTime fecha)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Where(f => f.FechaRegistro.Date == fecha.Date)
            .Include(f => f.Usuarios)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<decimal> ObtenerTotalVentasHoy()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        // Obtener todas las facturas del día
        var facturasHoy = await contexto.Facturas
            .Where(f => f.FechaRegistro.Date == DateTime.Today)
            .Select(f => f.FacturaId)
            .ToListAsync();

        if (!facturasHoy.Any())
        {
            return 0;
        }

        // Calcular el total sumando los subtotales de los detalles
        var totalVentas = await contexto.DetalleFacturas
            .Where(d => facturasHoy.Contains(d.FacturaId))
            .SumAsync(d => d.Cantidad * d.PrecioUnitario);

        return totalVentas;
    }
}