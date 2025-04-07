using CyH_Techno_Store.Models;
using CyH_Techno_Store.Services;
using System.Collections.Concurrent;

public class CarritoService
{
    private readonly ConcurrentDictionary<int, ItemCarrito> _items = new();

    public IEnumerable<ItemCarrito> Items => _items.Values;
    public decimal Total => _items.Values.Sum(i => i.Precio * i.Cantidad);
    public int CantidadItems => _items.Values.Sum(i => i.Cantidad);
    public bool TieneItems => _items.Any();

    public void AgregarProducto(Productos producto, int cantidad = 1)
    {
        if (producto == null) throw new ArgumentNullException(nameof(producto));
        if (cantidad <= 0) throw new ArgumentOutOfRangeException(nameof(cantidad));

        _items.AddOrUpdate(
            producto.ProductoId,
            _ => new ItemCarrito
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Precio = producto.PrecioUnitario,
                Cantidad = cantidad,
                ImagenUrl = producto.ImagenUrl,
                StockDisponible = producto.Stock
            },
            (_, itemExistente) =>
            {
                itemExistente.Cantidad += cantidad;
                return itemExistente;
            });
    }

    public bool EliminarProducto(int productoId)
    {
        return _items.TryRemove(productoId, out _);
    }

    public bool ActualizarCantidad(int productoId, int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
            return EliminarProducto(productoId);

        if (_items.TryGetValue(productoId, out var item))
        {
            item.Cantidad = nuevaCantidad;
            return true;
        }
        return false;
    }

    public void LimpiarCarrito()
    {
        _items.Clear();
    }

    public bool DisminuirCantidad(int productoId, int cantidad = 1)
    {
        if (_items.TryGetValue(productoId, out var item))
        {
            var nuevaCantidad = item.Cantidad - cantidad;
            if (nuevaCantidad <= 0)
                return EliminarProducto(productoId);

            item.Cantidad = nuevaCantidad;
            return true;
        }
        return false;
    }

    public bool AumentarCantidad(int productoId, int cantidad = 1, int? maxStock = null)
    {
        if (_items.TryGetValue(productoId, out var item))
        {
            if (maxStock.HasValue && item.Cantidad + cantidad > maxStock)
                return false;

            item.Cantidad += cantidad;
            return true;
        }
        return false;
    }

    public async Task<bool> VerificarDisponibilidad(ProductosService productosService)
    {
        foreach (var item in _items.Values)
        {
            var productoBD = await productosService.Buscar(item.ProductoId);
            if (productoBD == null || item.Cantidad > productoBD.Stock)
            {
                return false;
            }
        }

        return true;
    }
}

public class ItemCarrito
{
    public int ProductoId { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string? ImagenUrl { get; set; }
    public int StockDisponible { get; set; }
}