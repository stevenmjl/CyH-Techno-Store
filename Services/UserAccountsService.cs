using CyH_Techno_Store.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CyH_Techno_Store.Models;
using Microsoft.Extensions.Logging;

namespace CyH_Techno_Store.Services;

public class UserAccountsService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ILogger<UserAccountsService> _logger;

    public UserAccountsService(IDbContextFactory<Contexto> dbFactory, ILogger<UserAccountsService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> Existe(int id, string userName)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss
           .AnyAsync(u => u.Id != id && u.UserName!.ToLower() == userName.ToLower());
    }

    private async Task<bool> Insertar(UserAccounts userAccount)
    {
        _logger.LogInformation("Insertando nueva cuenta de usuario");
        await using var contexto = await _dbFactory.CreateDbContextAsync();

       
        userAccount.FechaRegistro = DateTime.Now;

        contexto.UserAccountss.Add(userAccount);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(UserAccounts userAccount)
    {
        _logger.LogInformation("Modificando cuenta de usuario existente");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Update(userAccount);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(UserAccounts userAccount)
    {
        if (!await Existe(userAccount.Id))
        {
            return await Insertar(userAccount);
        }
        else
        {
            return await Modificar(userAccount);
        }
    }

    public async Task<UserAccounts?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserAccounts?> BuscarPorUsuario(string userName)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<UserAccounts>> Listar(Expression<Func<UserAccounts, bool>> criterio)
    {
        _logger.LogInformation("Listando cuentas de usuario");
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<UserAccounts?> Autenticar(string userName, string password)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.UserAccountss
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
    }
}