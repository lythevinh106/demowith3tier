using AutoMapper;
using Castle.Core.Internal;
using DTOShared.FetchData;
using DTOShared.Pagging;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;

namespace MydemoFirst.Services.Application.Product.Queries
{
    public class FetchProductQuery : IRequest<PagedList<Models.Modules.Products.Models.Product>>
    //, ICacheableRequest

    {
        private readonly FetchDataProductRequest _fetchDataProductRequest;

        public FetchProductQuery(FetchDataProductRequest fetchDataProductRequest)
        {
            _fetchDataProductRequest = fetchDataProductRequest;
        }


        //implement from  ICacheableRequest
        //public TimeSpan Exp => TimeSpan.FromMinutes(20);

        //string ICacheableRequest.KeyCache => "AllProduct";





        public class Handler : BaseHandler, IRequestHandler<FetchProductQuery, PagedList<Models.Modules.Products.Models.Product>>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<PagedList<Models.Modules.Products.Models.Product>> Handle(FetchProductQuery request, CancellationToken cancellationToken)
            {
                var productRepo = _unitOfWork.ProductGenericReponsitory;



                IQueryable<Models.Modules.Products.Models.Product> quertListProduct = productRepo.All();

                if (request._fetchDataProductRequest.CategoryId.HasValue)
                {
                    quertListProduct = productRepo
                    .Filter(p => p.CategoryId == request._fetchDataProductRequest.CategoryId, quertListProduct);
                }


                if (!request._fetchDataProductRequest.Search.IsNullOrEmpty())
                {
                    quertListProduct = productRepo
                    .Search(p => p.Name.Contains(request._fetchDataProductRequest.Search), quertListProduct);
                }


                if (request._fetchDataProductRequest.Sort.HasValue)
                {
                    if (request._fetchDataProductRequest.Sort.Value.ToString() == "PriceDescending")
                    {
                        quertListProduct = productRepo.Sort(p => (p.Price).ToString(), quertListProduct, false);
                    }
                    if (request._fetchDataProductRequest.Sort.Value.ToString() == "PriceAscending")
                    {
                        quertListProduct = productRepo.Sort(p => (p.Price).ToString(), quertListProduct, true);
                    }
                }





                FetchDataRequest fetchDataRequest = _mapper.Map<FetchDataRequest>(request._fetchDataProductRequest);

                PagedList<Models.Modules.Products.Models.Product> dataResponse = productRepo.FectchData(fetchDataRequest, quertListProduct);

                return dataResponse;
            }
        }
    }
}