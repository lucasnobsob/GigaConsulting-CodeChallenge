using System.ComponentModel.DataAnnotations;

namespace GigaConsulting.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
