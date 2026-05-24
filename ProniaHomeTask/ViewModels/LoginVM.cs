using System.ComponentModel.DataAnnotations;

namespace ProniaHomeTask.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email daxil edin")]
        [EmailAddress(ErrorMessage = "Düzgün email formatı daxil edin")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifrə daxil edin")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; } = null!;

        [Display(Name = "Məni xatırla")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
