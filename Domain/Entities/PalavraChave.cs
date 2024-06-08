namespace Domain.Entities;

public class PalavraChave
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ICollection<Anime> Animes { get; set; } = [];
}