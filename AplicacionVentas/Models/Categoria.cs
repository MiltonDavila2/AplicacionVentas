using System.ComponentModel.DataAnnotations;

namespace AplicacionVentas.Models
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }

        public string nombre { get; set; }

        public string Slug { get; set; }
    }
}
