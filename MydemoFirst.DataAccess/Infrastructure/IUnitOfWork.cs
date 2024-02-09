using MydemoFirst.DataAccess.Repositories;
using MydemoFirst.Models;

namespace MydemoFirst.DataAccess.Infrastructure
{


    public interface IUnitOfWork
    {

        ProductGenericReponsitory ProductGenericReponsitory { get; }

        CateogoryGenericRepository CateogoryGenericRepository { get; }

        RoomGenericResponsitory RoomGenericResponsitory { get; }

        RoomMemberGenericResponsitory RoomMemberGenericResponsitory { get; }


        void SaveChanges();

    }

    public class UnitOfWork : IUnitOfWork
    {



        private MyDemoFirstWith3TierContext context;

        public UnitOfWork(MyDemoFirstWith3TierContext context)
        {
            this.context = context;
        }


        private CateogoryGenericRepository cateogoryGenericRepository;

        public CateogoryGenericRepository CateogoryGenericRepository

        {
            get
            {
                if (productGenericReponsitory == null)
                {
                    cateogoryGenericRepository = new CateogoryGenericRepository(context);
                }

                return cateogoryGenericRepository;
            }

        }



        private ProductGenericReponsitory productGenericReponsitory;

        public ProductGenericReponsitory ProductGenericReponsitory

        {
            get
            {
                if (productGenericReponsitory == null)
                {
                    productGenericReponsitory = new ProductGenericReponsitory(context);
                }

                return productGenericReponsitory;
            }

        }



        private RoomGenericResponsitory roomGenericReponsitory;

        public RoomGenericResponsitory RoomGenericResponsitory

        {
            get
            {
                if (roomGenericReponsitory == null)
                {
                    roomGenericReponsitory = new RoomGenericResponsitory(context);
                }

                return roomGenericReponsitory;
            }

        }


        private RoomMemberGenericResponsitory roomMemberGenericResponsitory;

        public RoomMemberGenericResponsitory RoomMemberGenericResponsitory

        {
            get
            {
                if (roomMemberGenericResponsitory == null)
                {
                    roomMemberGenericResponsitory = new RoomMemberGenericResponsitory(context);
                }

                return roomMemberGenericResponsitory;
            }

        }






        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}

