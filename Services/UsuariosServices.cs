using CyH_Techno_Store.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CyH_Techno_Store.Models;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class UsuariosService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<UsuariosService> _logger;

    public UsuariosService(IDbContextFactory<Contexto> dbFactory, ILogger<UsuariosService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios.AnyAsync(u => u.UsuarioId == id);
    }

    public async Task<bool> Existe(int id, string userName)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
           .AnyAsync(u => u.UsuarioId != id && u.UserName!.ToLower() == userName.ToLower());
    }

    private async Task<bool> Insertar(Usuarios userAccount)
    {
        _logger.LogInformation("Insertando nueva cuenta de usuario");
        await using var contexto = await _dbFactory.CreateDbContextAsync();


        userAccount.FechaRegistro = DateTime.Now;

        contexto.Usuarios.Add(userAccount);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Usuarios userAccount)
    {
        _logger.LogInformation("Modificando cuenta de usuario existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Update(userAccount);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Usuarios userAccount)
    {
        if (!await Existe(userAccount.UsuarioId))
        {
            return await Insertar(userAccount);
        }
        else
        {
            return await Modificar(userAccount);
        }
    }

    public async Task<Usuarios?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.UsuarioId == id);
    }

    public async Task<Usuarios?> BuscarPorUsuario(string userName)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .Where(u => u.UsuarioId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Usuarios>> Listar(Expression<Func<Usuarios, bool>> criterio)
    {
        _logger.LogInformation("Listando cuentas de usuario");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<Usuarios?> Autenticar(string userName, string password)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
    }
}

