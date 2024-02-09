using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.Products.Response
{
    public class ProductResponse
    {

        public int Id { get; set; }

        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]

        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
