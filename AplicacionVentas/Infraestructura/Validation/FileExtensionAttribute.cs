using System.ComponentModel.DataAnnotations;

namespace AplicacionVentas.Infraestructura.Validation
{
    public class FileExtensionAttribute : ValidationAttribute 
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "png" };

                bool result = extensions.Any(x=>extension.EndsWith(x));

                if(!result)
                {
                    return new ValidationResult("Las extensiones de la imagen son jpg o png");
                }
            }

            return ValidationResult.Success;
        }
    }
}
