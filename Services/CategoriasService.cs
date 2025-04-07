using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyH_Techno_Store.Services;

public class CategoriasService(IDbContextFactory<Contexto> dbFactory)
{
    private async Task<bool> Existe(int categoriaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias
            .AnyAsync(c => c.CategoriaId == categoriaId);
    }

    public async Task<bool> Existe(int categoriaId, string? nombre)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias
            .AnyAsync(c => c.CategoriaId != categoriaId &&
                          c.Nombre!.ToLower() == nombre!.ToLower());
    }

    private async Task<bool> Insertar(Categoria categoria)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Categorias.Add(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Categoria categoria)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Categorias.Update(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Categoria categoria)
    {
        if (!await Existe(categoria.CategoriaId))
            return await Insertar(categoria);
        else
            return await Modificar(categoria);
    }

    public async Task<Categoria?> Buscar(int categoriaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias
            .FirstOrDefaultAsync(c => c.CategoriaId == categoriaId);
    }

    public async Task<bool> Eliminar(int categoriaId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        // Verificar productos asociados
        var tieneProductos = await contexto.Productos
            .AnyAsync(p => p.CategoriaId == categoriaId);

        if (tieneProductos)
            return false;

        return await contexto.Categorias
            .Where(c => c.CategoriaId == categoriaId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Categoria>> Listar(Expression<Func<Categoria, bool>> criterio,
    bool incluirProductos = false)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var query = contexto.Categorias.AsQueryable();

        if (incluirProductos)
        {
            query = query.Include(c => c.Productos);
        }

        return await query
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> UltimoId()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var ultimaCategoria = await contexto.Categorias
            .OrderByDescending(c => c.CategoriaId)
            .FirstOrDefaultAsync();

        return ultimaCategoria?.CategoriaId ?? 0;
    }

    public async Task<List<Categoria>> ObtenerCategoriasActivas()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias
            .Where(c => c.Productos.Any())
            .ToListAsync();
    }
}