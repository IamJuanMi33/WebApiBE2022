using DogsWebAPISeg.Validations;
using System.ComponentModel.DataAnnotations;

namespace DogsWebAPISeg.DTOs
{
    public class DogDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede contener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }
    }
}
