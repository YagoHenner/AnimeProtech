using Domain.Entities;

namespace Application.Interfaces;

public interface IAnimeRepository
{
    Task AddAsync(Anime anime);
    Task<List<Anime>> GetAnimes();
    Task<Anime?> GetAnimeById(int id);
    Task DeleteAnime(Anime anime);

    Task UpdateAnime(Anime anime);
}
