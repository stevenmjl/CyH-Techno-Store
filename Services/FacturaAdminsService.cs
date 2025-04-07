using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace CyH_Techno_Store.Services;

public class FacturaAdminsService(IDbContextFactory<Contexto> dbFactory, TransaccionesService transaccionesService)
{
    private async Task<bool> Existe(int facturaAdminId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.FacturaAdmins
            .AnyAsync(f => f.FacturaAdminId == facturaAdminId);
    }

    private async Task<bool> Insertar(FacturaAdmins factura)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            contexto.FacturaAdmins.Add(factura);
            await contexto.SaveChangesAsync();

            foreach (var detalle in factura.DetalleFacturaAdmin)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock += detalle.Cantidad;
                }
            }

            var transaccion = new Transacciones
            {
                Monto = factura.DetalleFacturaAdmin.Sum(d => d.Subtotal),
                FechaRegistro = DateTime.Now,
                Tipo = "Gasto",
                FacturaId = factura.FacturaAdminId,
                FacturaAdmins = factura
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

    private async Task<bool> Modificar(FacturaAdmins factura)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            // Obtener detalles antiguos
            var detallesAnteriores = await contexto.DetalleFacturaAdmins
                .Where(d => d.FacturaAdminId == factura.FacturaAdminId)
                .ToListAsync();

            // Actualizar factura
            contexto.FacturaAdmins.Update(factura);
            await contexto.SaveChangesAsync();

            // Recalcular stock
            foreach (var detalle in detallesAnteriores)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= detalle.Cantidad;
                }
            }

            foreach (var detalle in factura.DetalleFacturaAdmin)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock += detalle.Cantidad;
                }
            }

            // Actualizar transacción
            await transaccionesService.EliminarTransaccion(factura.FacturaAdminId);

            var nuevaTransaccion = new Transacciones
            {
                Monto = factura.DetalleFacturaAdmin.Sum(d => d.Subtotal),
                FechaRegistro = DateTime.Now,
                Tipo = "Gasto",
                FacturaId = factura.FacturaAdminId,
                FacturaAdmins = factura
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

    public async Task<bool> Guardar(FacturaAdmins factura)
    {
        if (!await Existe(factura.FacturaAdminId))
            return await Insertar(factura);
        else
            return await Modificar(factura);
    }

    public async Task<FacturaAdmins?> Buscar(int facturaAdminId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.FacturaAdmins
            .Include(f => f.DetalleFacturaAdmin)
                .ThenInclude(d => d.Productos)
            .Include(f => f.Proveedores)
            .Include(f => f.Usuarios)
            .FirstOrDefaultAsync(f => f.FacturaAdminId == facturaAdminId);
    }

    public async Task<bool> Eliminar(int facturaAdminId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        try
        {
            // Revertir stock
            var detalles = await contexto.DetalleFacturaAdmins
                .Where(d => d.FacturaAdminId == facturaAdminId)
                .ToListAsync();

            foreach (var detalle in detalles)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= detalle.Cantidad;
                }
            }

            // Eliminar transacción asociada
            await transaccionesService.EliminarTransaccion(facturaAdminId);

            // Eliminar factura
            await contexto.FacturaAdmins
                .Where(f => f.FacturaAdminId == facturaAdminId)
                .ExecuteDeleteAsync();

            await contexto.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<FacturaAdmins>> Listar(
        Expression<Func<FacturaAdmins, bool>>? criterio = null,
        bool incluirDetalles = false)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var query = contexto.FacturaAdmins.AsQueryable();

        if (incluirDetalles)
        {
            query = query
                .Include(f => f.DetalleFacturaAdmin)
                .Include(f => f.Proveedores)
                .Include(f => f.Usuarios);
        }

        if (criterio != null)
            query = query.Where(criterio);

        return await query.ToListAsync();
    }

    public async Task<decimal> CalcularTotalFactura(int facturaAdminId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.DetalleFacturaAdmins
            .Where(d => d.FacturaAdminId == facturaAdminId)
            .SumAsync(d => d.Subtotal);
    }
}