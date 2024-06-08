using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AnimeRepository : IAnimeRepository
{
    private readonly AnimeDbContext _context;

    public AnimeRepository(AnimeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Anime anime)
    {
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Anime>> GetAnimes()
    {
        return await _context.Animes.ToListAsync();
    }

    public async Task<Anime?> GetAnimeById(int id)
    {
        return await _context.Animes.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task DeleteAnime(Anime anime)
    {
        _context.Animes.Remove(anime);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAnime(Anime anime)
    {
        _context.Entry(anime).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    
}
