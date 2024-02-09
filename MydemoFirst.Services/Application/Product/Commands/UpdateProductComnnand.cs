using AutoMapper;
using DTOShared.Modules.Products.Request;
using DTOShared.Modules.Products.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Product.Command
{


    public class UpdateProductComnnand : IRequest<ProductResponse>
    {

        private readonly ProductRequest _productRequest;

        private readonly int _productId;
        public UpdateProductComnnand(int productId, ProductRequest productRequest)
        {

            _productRequest = productRequest;

            _productId = productId;

        }

        public class Handler : BaseHandler, IRequestHandler<UpdateProductComnnand, ProductResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<ProductResponse> Handle(UpdateProductComnnand request, CancellationToken cancellationToken)
            {



                if (!await _unitOfWork.CateogoryGenericRepository.CheckExist(c => c.Id == request._productRequest.CategoryId))
                {
                    throw new ValidationException("Category does not exist.");
                }

                if (!await _unitOfWork.ProductGenericReponsitory.CheckExist(p => p.Id == request._productId))
                {
                    throw new ValidationException("Product does not exist.");
                }






                var newProduct = _mapper.Map<Models.Modules.Products.Models.Product>(request._productRequest);

                newProduct.Id = request._productId;

                Models.Modules.Products.Models.Product product = _unitOfWork.ProductGenericReponsitory.Update(newProduct);
                _unitOfWork.SaveChanges();

                return _mapper.Map<ProductResponse>(product);
            }
        }


    }



}
