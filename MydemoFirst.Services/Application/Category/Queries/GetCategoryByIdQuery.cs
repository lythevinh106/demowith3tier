using AutoMapper;
using DTOShared.Modules.Category.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Category.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponse>
    {
        private readonly int _categoryId;

        public GetCategoryByIdQuery(int categoryId)
        {
            _categoryId = categoryId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                if (!await _unitOfWork.CateogoryGenericRepository.CheckExist(c => c.Id == request._categoryId))
                {
                    throw new ValidationException("Category does not exist.");
                }

                Models.Modules.Category.Models.Category category = await _unitOfWork.CateogoryGenericRepository.Get(request._categoryId);

                return _mapper.Map<CategoryResponse>(category);
            }
        }
    }
}