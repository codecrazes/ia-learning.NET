using ia_learning.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Recomendacao
{
    public int Id { get; set; }

    [Column(TypeName = "NCLOB")]
    public string Mensagem { get; set; }
    public DateTime Data { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}
