using ia_learning.Models;

public class Habilidade
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ICollection<UsuarioHabilidade> UsuarioHabilidades { get; set; } = new List<UsuarioHabilidade>();
}
