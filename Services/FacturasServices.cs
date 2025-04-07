using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class FacturasService(IDbContextFactory<Contexto> dbFactory, TransaccionesService transaccionesService)
{
    private async Task<bool> Existe(int facturaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .AnyAsync(f => f.FacturaId == facturaId);
    }

    private async Task<bool> ValidarStockDisponible(Facturas factura)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        foreach (var detalle in factura.DetalleFacturas)
        {
            var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
            if (producto == null || producto.Stock < detalle.Cantidad)
            {
                return false;
            }
        }
        return true;
    }

    private async Task<bool> Insertar(Facturas factura)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            // Validar stock antes de proceder
            if (!await ValidarStockDisponible(factura))
                return false;

            contexto.Facturas.Add(factura);
            await contexto.SaveChangesAsync();

            foreach (var detalle in factura.DetalleFacturas)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= detalle.Cantidad;
                }
            }

            var transaccion = new Transacciones
            {
                Monto = factura.DetalleFacturas.Sum(d => d.Subtotal),
                FechaRegistro = DateTime.Now,
                Tipo = "Ingreso",
                FacturaId = factura.FacturaId,
                Facturas = factura
            };

            await transaccionesService.RegistrarTransaccion(transaccion);

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> Modificar(Facturas factura)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            // Validar nuevo stock
            if (!await ValidarStockDisponible(factura))
                return false;

            // Obtener detalles antiguos
            var detallesAnteriores = await contexto.DetalleFacturas
                .Where(d => d.FacturaId == factura.FacturaId)
                .ToListAsync();

            // Revertir stock de detalles anteriores
            foreach (var detalle in detallesAnteriores)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock += detalle.Cantidad;
                }
            }

            // Actualizar factura
            contexto.Facturas.Update(factura);
            await contexto.SaveChangesAsync();

            // Aplicar nuevos detalles (disminuir stock)
            foreach (var detalle in factura.DetalleFacturas)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= detalle.Cantidad;
                }
            }

            // Actualizar transacción
            await transaccionesService.EliminarTransaccion(factura.FacturaId);

            var nuevaTransaccion = new Transacciones
            {
                Monto = factura.DetalleFacturas.Sum(d => d.Subtotal),
                FechaRegistro = DateTime.Now,
                Tipo = "Ingreso",
                FacturaId = factura.FacturaId,
                Facturas = factura
            };

            await transaccionesService.RegistrarTransaccion(nuevaTransaccion);

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Guardar(Facturas factura)
    {
        if (!await Existe(factura.FacturaId))
            return await Insertar(factura);
        else
            return await Modificar(factura);
    }

    public async Task<Facturas?> Buscar(int facturaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Include(f => f.DetalleFacturas)
                .ThenInclude(d => d.Productos)
            .Include(f => f.Usuarios)
            .FirstOrDefaultAsync(f => f.FacturaId == facturaId);
    }

    public async Task<bool> Eliminar(int facturaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            // Revertir stock (aumentar)
            var detalles = await contexto.DetalleFacturas
                .Where(d => d.FacturaId == facturaId)
                .ToListAsync();

            foreach (var detalle in detalles)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock += detalle.Cantidad;
                }
            }

            // Eliminar transacción asociada
            await transaccionesService.EliminarTransaccion(facturaId);

            // Eliminar factura
            await contexto.Facturas
                .Where(f => f.FacturaId == facturaId)
                .ExecuteDeleteAsync();

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Facturas>> Listar(
        Expression<Func<Facturas, bool>>? criterio = null,
        bool incluirDetalles = false)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var query = contexto.Facturas.AsQueryable();

        if (incluirDetalles)
        {
            query = query
                .Include(f => f.DetalleFacturas)
                .Include(f => f.Usuarios);
        }

        if (criterio != null)
            query = query.Where(criterio);

        return await query.ToListAsync();
    }

    public async Task<decimal> CalcularTotalFactura(int facturaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturas
            .Where(d => d.FacturaId == facturaId)
            .SumAsync(d => d.Subtotal);
    }

    public async Task<List<Facturas>> ListarPorUsuario(int usuarioId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Facturas
            .Where(f => f.UsuarioId == usuarioId)
            .Include(f => f.DetalleFacturas)
                .ThenInclude(d => d.Productos)
            .OrderByDescending(f => f.FechaRegistro)
            .ToListAsync();
    }
}