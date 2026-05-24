using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProniaHomeTask.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage ="Məhsul adını daxil edin!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Məhsul adı minimum 3, maximum 30 simvol olmalıdır!")]
        [Display(Name="Məhsulun Adı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Qiymət daxil edin!")]
        [Range(0.01, 999.0, ErrorMessage = "Qiymət 0.01 və 999 arasında olmalıdır!")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Qiymət")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Məhsulun açıqlamasını daxil edin!")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Məhsulun açıqlaması 10-150 simvol aralığında olmalıdır!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Məhsul üçün mütləq bir kateqoriya seçilməlidir.")]
        [Display(Name = "Kateqoriya")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
        public string ImgUrl { get; set; } = "uploads/products/no-image.svg";
        [NotMapped]
        [Display(Name = "Məhsul şəkli")]
        public IFormFile? ImageUpload { get; set; }
    }
}