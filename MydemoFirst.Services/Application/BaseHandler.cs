using AutoMapper;
using MydemoFirst.DataAccess.Infrastructure;

namespace MydemoFirst.Services.Application
{
    public class BaseHandler
    {
        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        private IUnitOfWork unitOfWork;

        public BaseHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public BaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
    }
}
