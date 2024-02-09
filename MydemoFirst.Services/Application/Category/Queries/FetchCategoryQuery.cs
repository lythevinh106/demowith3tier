using AutoMapper;
using Castle.Core.Internal;
using DTOShared.FetchData;
using DTOShared.Pagging;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;

namespace MydemoFirst.Services.Application.Category.Queries
{
    public class FetchCategoryQuery : IRequest<PagedList<Models.Modules.Category.Models.Category>>
    {
        private readonly FetchDataCategoryRequest _fetchDataCategoryRequest;

        public FetchCategoryQuery(FetchDataCategoryRequest fetchDataCategoryRequest)
        {
            _fetchDataCategoryRequest = fetchDataCategoryRequest;
        }

        public class Handler : BaseHandler, IRequestHandler<FetchCategoryQuery, PagedList<Models.Modules.Category.Models.Category>>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<PagedList<Models.Modules.Category.Models.Category>> Handle(FetchCategoryQuery request, CancellationToken cancellationToken)
            {
                var CategoryRepo = _unitOfWork.CateogoryGenericRepository;

                IQueryable<Models.Modules.Category.Models.Category> quertListCategory = CategoryRepo.All();



                if (!request._fetchDataCategoryRequest.Search.IsNullOrEmpty())
                {
                    quertListCategory = CategoryRepo
                    .Search(c => c.Name.Contains(request._fetchDataCategoryRequest.Search), quertListCategory);
                }

                if (request._fetchDataCategoryRequest.Sort.HasValue)
                {
                    if (request._fetchDataCategoryRequest.Sort.Value.ToString() == "IdDescending")
                    {
                        quertListCategory = CategoryRepo.Sort(c => (c.Id).ToString(), quertListCategory, false);
                    }
                    if (request._fetchDataCategoryRequest.Sort.Value.ToString() == "IdAscending")
                    {
                        quertListCategory = CategoryRepo.Sort(c => (c.Id).ToString(), quertListCategory, true);
                    }
                }

                FetchDataRequest fetchDataRequest = _mapper.Map<FetchDataRequest>(request._fetchDataCategoryRequest);

                PagedList<Models.Modules.Category.Models.Category> dataResponse = CategoryRepo.FectchData(fetchDataRequest, quertListCategory);

                return dataResponse;
            }
        }
    }
}