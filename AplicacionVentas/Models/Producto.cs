
using AplicacionVentas.Infraestructura.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionVentas.Models
{
    public class Producto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Ingrese porfavor un valor")]
        public string Nombre { get; set; }

        public string Slug {  get; set; }

        [Required,MinLength(4,ErrorMessage = "El tamaño minimo es 4 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage = "Ingrese porfavor un valor")]
        [Column(TypeName ="decimal(8,2)")]
        public decimal Precio { get; set; }


        [Required, Range(1,int.MaxValue,ErrorMessage ="Elija una categoria")]
        public long CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public string Imagen { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
 