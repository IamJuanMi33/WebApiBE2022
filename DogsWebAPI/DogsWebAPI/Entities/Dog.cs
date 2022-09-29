using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogsWebAPI.Entities
{
    public class Dog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:5, ErrorMessage = "El campo {0} solo puede contener hasta 5 caracteres")]
        public string Name { get; set; }

        [Range(1, 18, ErrorMessage = "El campo Edad no se encuentra dentro del rango")]
        [NotMapped]
        public int Age { get; set; }

        [CreditCard]
        [NotMapped]
        public string CardNum { get; set; }

        [Url]
        [NotMapped]
        public string Url { get; set; }
        public List<Kennel> kennels { get; set; }
    }
}
