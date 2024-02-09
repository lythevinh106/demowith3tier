using AutoMapper;
using DTOShared.Modules.Products.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        private readonly int _productId;

        public GetProductByIdQuery(int productId)
        {
            _productId = productId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetProductByIdQuery, ProductResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                if (!await _unitOfWork.ProductGenericReponsitory.CheckExist(c => c.Id == request._productId))
                {
                    throw new ValidationException("Product does not exist.");
                }

                Models.Modules.Products.Models.Product product = await _unitOfWork.ProductGenericReponsitory.Get(request._productId);

                return _mapper.Map<ProductResponse>(product);
            }
        }
    }
}