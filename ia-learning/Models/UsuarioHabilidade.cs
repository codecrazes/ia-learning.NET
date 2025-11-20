using ia_learning.Models;

namespace ia_learning.Models
{
    public class UsuarioHabilidade
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int HabilidadeId { get; set; }
        public Habilidade Habilidade { get; set; }
    }
}
