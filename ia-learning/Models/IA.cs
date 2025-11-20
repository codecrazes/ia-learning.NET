namespace ia_learning.Models
{
    public class IA
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Provedor { get; set; }
        public string Descricao { get; set; }
        public decimal Custo { get; set; }
        public string Tipo { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}
