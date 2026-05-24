

namespace ProniaHomeTask.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Product { get; set; }
    }
}
