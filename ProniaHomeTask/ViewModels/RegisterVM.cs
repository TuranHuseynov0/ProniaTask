using System.ComponentModel.DataAnnotations;

namespace ProniaHomeTask.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Ad və soyad daxil edin")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad minimum 2 simvol olmalıdır")]
        [Display(Name = "Ad və soyad")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email daxil edin")]
        [EmailAddress(ErrorMessage = "Düzgün email formatı daxil edin")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifrə daxil edin")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifrə minimum 6 simvol olmalıdır")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Şifrəni təkrar daxil edin")]
        [Compare(nameof(Password), ErrorMessage = "Şifrələr uyğun gəlmir")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrəni təkrarla")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
