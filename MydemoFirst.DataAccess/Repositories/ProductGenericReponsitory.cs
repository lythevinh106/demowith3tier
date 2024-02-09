using MydemoFirst.DataAccess.Repositories.Generic;
using MydemoFirst.Models;
using MydemoFirst.Models.Modules.Products.Models;

namespace MydemoFirst.DataAccess.Repositories
{
    public class ProductGenericReponsitory : GenetricRepository<Product>
    {
        public ProductGenericReponsitory(MyDemoFirstWith3TierContext context) : base(context)
        {

        }



    }
}
