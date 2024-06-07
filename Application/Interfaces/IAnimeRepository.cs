using Domain.Entities;

namespace Application.Interfaces;

public interface IAnimeRepository
{
    Task AddAsync(Anime anime);
}
