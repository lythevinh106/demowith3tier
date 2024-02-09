using AutoMapper;
using DTOShared.Modules.Products.Request;
using DTOShared.Modules.Products.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Product.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>, IRemoveableCache
    {
        private readonly ProductRequest
            _productRequest;

        public CreateProductCommand(ProductRequest productRequest)
        {
            _productRequest = productRequest;
        }

        public string KeyCache => "AllProduct";

        public class Handler : BaseHandler, IRequestHandler<CreateProductCommand, ProductResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if (!await _unitOfWork.CateogoryGenericRepository.CheckExist(c => c.Id == request._productRequest.CategoryId))
                {
                    throw new ValidationException("Category does not exist.");
                }

                var newProduct = _mapper.Map<Models.Modules.Products.Models.Product>(request._productRequest);

                Models.Modules.Products.Models.Product product = await _unitOfWork.ProductGenericReponsitory.Add(newProduct);

                _unitOfWork.SaveChanges();

                return _mapper.Map<ProductResponse>(product);
            }
        }
    }
}