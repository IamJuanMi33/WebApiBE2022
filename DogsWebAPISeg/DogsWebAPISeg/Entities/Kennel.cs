using DogsWebAPISeg.Validations;
using System.ComponentModel.DataAnnotations;

namespace DogsWebAPISeg.Entities
{
    public class Kennel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede contener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public DateTime? DateCreation { get; set; } 

        public List<DogKennel> DogKennel { get; set; }
    }
}
