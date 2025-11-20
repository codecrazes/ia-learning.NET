namespace ia_learning.DTOs
{
    public class RecomendacaoDto
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }
        public int IAId { get; set; }
    }
}
