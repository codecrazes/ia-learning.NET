using ia_learning.Models;

public class Tarefa
{
    public int Id { get; set; }

    public string Titulo { get; set; }       
    public int Dificuldade { get; set; }    
    public int TempoDisponivelMin { get; set; } 

    public string Descricao { get; set; }
    public DateTime DataExecucao { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public int IAId { get; set; }
    public IA IA { get; set; }
}
