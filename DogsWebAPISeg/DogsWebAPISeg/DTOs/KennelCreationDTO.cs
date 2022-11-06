using DogsWebAPISeg.Validations;
using System.ComponentModel.DataAnnotations;

namespace DogsWebAPISeg.DTOs
{
    public class KennelCreationDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede contener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public List<int> DogsIds { get; set; }

    }
}
