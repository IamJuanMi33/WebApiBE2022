using DogsWebAPISeg.Validations;
using System.ComponentModel.DataAnnotations;

namespace DogsWebAPISeg.Entities
{
    public class Dog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede contener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public List<DogKennel> DogKennel { get; set; }
    }
}
