﻿namespace Domain.Entities;

public class Anime
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Resumo { get; set; } = string.Empty;
    public List<string> PalavrasChave { get; set; } = [];
}
