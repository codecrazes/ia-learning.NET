namespace ia_learning.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        public int Nota { get; set; }
        public string Comentario { get; set; }
        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int IAId { get; set; }
        public IA IA { get; set; }
    }
}
