using AutoMapper;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;

namespace MydemoFirst.Services.Application.Category.Queries
{
    public class GetAllCategoryQuery : IRequest<List<Models.Modules.Category.Models.Category>>
    {
        public GetAllCategoryQuery()
        {
        }

        public class Handler : BaseHandler, IRequestHandler<GetAllCategoryQuery, List<Models.Modules.Category.Models.Category>>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<List<Models.Modules.Category.Models.Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                var CategoryRepo = _unitOfWork.CateogoryGenericRepository;

                IQueryable<Models.Modules.Category.Models.Category> quertListCategory = CategoryRepo.All();

                List<Models.Modules.Category.Models.Category> dataResponse = quertListCategory.ToList();

                return dataResponse;
            }
        }
    }
}