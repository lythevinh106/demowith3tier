using MydemoFirst.DataAccess.Repositories.Generic;
using MydemoFirst.Models;
using MydemoFirst.Models.Modules.RoomMember.Models;

namespace MydemoFirst.DataAccess.Repositories
{
    public class RoomMemberGenericResponsitory : GenetricRepository<RoomMember>
    {
        public RoomMemberGenericResponsitory(MyDemoFirstWith3TierContext context) : base(context)
        {


        }


        public int CountMembers(string roomId)
        {

            return _context.Set<RoomMember>().Where(rm => rm.RoomId == roomId).Count();

        }
    }
}
