namespace ia_learning.DTOs
{
    public class AvaliacaoDto
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }
        public int IAId { get; set; }
    }
}
