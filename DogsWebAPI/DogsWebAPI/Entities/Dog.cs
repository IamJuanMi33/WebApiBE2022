using DogsWebAPI.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogsWebAPI.Entities
{
    public class Dog : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:5, ErrorMessage = "El campo {0} solo puede contener hasta 5 caracteres")]
        [PrimeraLetraMayuscula]
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

        [NotMapped]
        public int Mayor { get; set; }

        [NotMapped]
        public int Menor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var primeraLetra = Name[0].ToString();


                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                        new String[] { nameof(Name) });
                }

            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser más grande que el campo Mayor",
                    new String[] { nameof(Menor) });
            }
        }
    }
}
