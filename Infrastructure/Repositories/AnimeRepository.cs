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
    public async Task<List<Anime>> GetAnimes()
    {
        return await _context.Animes.Include(a => a.PalavrasChave).ToListAsync();
    }
}
