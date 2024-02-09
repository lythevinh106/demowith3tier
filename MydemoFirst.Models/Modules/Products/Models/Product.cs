using DTOShared.Contracts;
using System.Collections;
using System.ComponentModel.DataAnnotations;
namespace MydemoFirst.Models.Modules.Products.Models
{
    public class Product : ISeedable, ICRUDTable
    {

        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }


        [Range(0, double.MaxValue)]

        public decimal Price { get; set; }


        public int CategoryId { get; set; }

        public virtual MydemoFirst.Models.Modules.Category.Models.Category Category { get; set; }


        public virtual List<MydemoFirst.Models.Modules.ProductOrder.Models.ProductOrder> ProductOrders { get; set; }
        public IList Seed()
        {
            var products = new List<Product>(
               Enumerable.Range(1, 15).Select(idx => new Product
               {
                   Id = idx,
                   Name = $"ProductName {idx}",
                   Price = 2536 + idx,
                   CategoryId = idx,

               })

               );
            return products;
        }
    }
}

