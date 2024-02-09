using Microsoft.EntityFrameworkCore;
using MydemoFirst.DataAccess.Repositories.Generic;
using MydemoFirst.Models;
using MydemoFirst.Models.Modules.Room.Models;

namespace MydemoFirst.DataAccess.Repositories
{
    public class RoomGenericResponsitory : GenetricRepository<Room>
    {
        public RoomGenericResponsitory(MyDemoFirstWith3TierContext context) : base(context)
        {
        }



        public async Task<Room> GetByName(string name)
        {
            return await _context.Set<Room>().FirstOrDefaultAsync(x => x.Name == name);

        }
    }
}
