using DTOShared.Contracts;
using MydemoFirst.Models.Modules.Products.Models;

namespace MydemoFirst.Models.Modules.ProductOrder.Models
{
    public class ProductOrder : ISeedable, ICRUDTable
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual MydemoFirst.Models.Modules.Order.Models.Order Order { get; set; }





        System.Collections.IList ISeedable.Seed()
        {
            var pOrders = new List<ProductOrder>(
              Enumerable.Range(1, 15).Select(idx => new ProductOrder
              {
                  ProductId = idx,
                  OrderId = idx,
                  Quantity = new Random().Next(1, 50),


              })

              );
            return pOrders;

        }
    }
}
