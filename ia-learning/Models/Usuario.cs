namespace ia_learning.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public ICollection<UsuarioHabilidade> UsuarioHabilidades { get; set; } = new List<UsuarioHabilidade>();
        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        public ICollection<Recomendacao> Recomendacoes { get; set; } = new List<Recomendacao>(); 
    }
}
