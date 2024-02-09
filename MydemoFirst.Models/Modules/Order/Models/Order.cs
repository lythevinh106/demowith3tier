using DTOShared.Contracts;
using DTOShared.Enums;

namespace MydemoFirst.Models.Modules.Order.Models
{
    public class Order : ISeedable, ICRUDTable
    {


        public int Id { get; set; }



        public StatusOrder Status { get; set; } = StatusOrder.Pending;

        public string UserId { get; set; }

        public virtual List<MydemoFirst.Models.Modules.ProductOrder.Models.ProductOrder> ProductOrders { get; set; }

        public virtual MydemoFirst.Models.Modules.User.Models.User User { get; set; }

        System.Collections.IList ISeedable.Seed()
        {
            var orders = new List<Order>(
              Enumerable.Range(1, 15).Select(idx => new Order
              {
                  Id = idx,
                  Status = StatusOrder.Pending,
                  UserId = "79640b64-94d3-4cb2-89c8-a5fefe3c2051",




              })

              );
            return orders;
        }
    }
}
