using System.ComponentModel.DataAnnotations;

namespace DogsWebAPISeg.DTOs
{
    public class EditAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
