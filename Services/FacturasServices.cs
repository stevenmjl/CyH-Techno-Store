using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class FacturassService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<FacturassService> _logger;

    public FacturassService(IDbContextFactory<Contexto> dbFactory, ILogger<FacturassService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass.AnyAsync(f => f.FacturasId == facturasId);
    }

    public async Task<bool> Existe(DateTime fecha, int usuarioId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass
           .AnyAsync(f => f.Fecha.Date == fecha.Date && f.UsuarioId == usuarioId);
    }

    private async Task<bool> Insertar(Facturass factura)
    {
        _logger.LogInformation("Insertando nueva factura");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        contexto.Facturass.Add(factura);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Facturass factura)
    {
        _logger.LogInformation("Modificando factura existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        contexto.Update(factura);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Facturass factura)
    {
        if (!await Existe(factura.FacturasId))
        {
            return await Insertar(factura);
        }
        else
        {
            return await Modificar(factura);
        }
    }

    public async Task<Facturass?> Buscar(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass
            .Include(f => f.Usuario) 
            .Include(f => f.Detalles) 
                .ThenInclude(d => d.Productos) 
            .FirstOrDefaultAsync(f => f.FacturasId == facturasId);
    }

    public async Task<bool> Eliminar(int facturasId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();

        await contexto.DetallesFacturas
            .Where(d => d.FacturasId == facturasId)
            .ExecuteDeleteAsync();

      
        return await contexto.Facturass
            .Where(f => f.FacturasId == facturasId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Facturass>> Listar(Expression<Func<Facturass, bool>> criterio)
    {
        _logger.LogInformation("Listando facturas");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass
            .Include(f => f.Usuario)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    
    public async Task<List<Facturass>> ListarPorUsuario(int usuarioId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass
            .Where(f => f.UsuarioId == usuarioId)
            .Include(f => f.Detalles)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Facturass>> ListarPorFecha(DateTime fecha)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass
            .Where(f => f.Fecha.Date == fecha.Date)
            .Include(f => f.Usuario)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<decimal> ObtenerTotalVentasHoy()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Facturass
            .Where(f => f.Fecha.Date == DateTime.Today)
            .SumAsync(f => f.Total);
    }
}