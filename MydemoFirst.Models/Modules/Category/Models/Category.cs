using DTOShared.Contracts;
using MydemoFirst.Models.Modules.Products.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Models.Modules.Category.Models
{
    public class Category : ISeedable, ICRUDTable
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }

        public IList Seed()
        {
            var categories = new List<Category>(

               Enumerable.Range(1, 15).Select(idx => new Category
               {
                   Id = idx,
                   Name = $"category name {idx}",
               })

               );

            return categories;
        }
    }
}