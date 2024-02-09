using MydemoFirst.DataAccess.Repositories.Generic;
using MydemoFirst.Models;
using MydemoFirst.Models.Modules.Category.Models;

namespace MydemoFirst.DataAccess.Repositories
{
    public class CateogoryGenericRepository : GenetricRepository<Category>
    {

        public CateogoryGenericRepository(MyDemoFirstWith3TierContext context) : base(context)
        {

        }



    }
}
