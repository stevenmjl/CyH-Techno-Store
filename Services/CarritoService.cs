using System;
using System.Collections.Generic;
using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;

public class CarritoService
{
    private readonly List<ItemCarrito> _items = new();
    private readonly Contexto _contexto;

    public CarritoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public IEnumerable<ItemCarrito> Items => _items;

    public void AgregarProducto(Productos producto, int cantidad = 1)
    {
        var itemExistente = _items.FirstOrDefault(i => i.ProductoId == producto.ProductoId);

        if (itemExistente != null)
        {
            itemExistente.Cantidad += cantidad;
        }
        else
        {
            _items.Add(new ItemCarrito
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Precio = producto.PrecioUnitario,
                Cantidad = cantidad,
                ImagenUrl = producto.ImagenUrl
            });
        }
    }

    public void EliminarProducto(int productoId)
    {
        var item = _items.FirstOrDefault(i => i.ProductoId == productoId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void ActualizarCantidad(int productoId, int nuevaCantidad)
    {
        var item = _items.FirstOrDefault(i => i.ProductoId == productoId);
        if (item != null)
        {
            item.Cantidad = nuevaCantidad;
        }
    }

    public void LimpiarCarrito()
    {
        _items.Clear();
    }

    public decimal Total => _items.Sum(i => i.Precio * i.Cantidad);

    public int CantidadItems => _items.Sum(i => i.Cantidad);
}

public class ItemCarrito
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string ImagenUrl { get; set; }
}