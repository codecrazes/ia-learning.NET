namespace ia_learning.DTOs
{
    public class TarefaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Dificuldade { get; set; }
        public int TempoDisponivelMin { get; set; }

        public string Descricao { get; set; }
        public DateTime DataExecucao { get; set; }

        public int UsuarioId { get; set; }
        public int IAId { get; set; }
    }
}
