using AutoMapper;
using DTOShared.Modules.Category.Request;
using DTOShared.Modules.Category.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Category.Command
{
    public class UpdateCategoryComnnand : IRequest<CategoryResponse>
    {
        private readonly CategoryRequest _categoryRequest;

        private readonly int _categoryId;

        public UpdateCategoryComnnand(int categoryId, CategoryRequest categoryRequest)
        {
            _categoryRequest = categoryRequest;

            _categoryId = categoryId;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdateCategoryComnnand, CategoryResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<CategoryResponse> Handle(UpdateCategoryComnnand request, CancellationToken cancellationToken)
            {
                if (!await _unitOfWork.CateogoryGenericRepository.CheckExist(c => c.Id == request._categoryId))
                {
                    throw new ValidationException("Category does not exist.");
                }

                var newCategory = _mapper.Map<Models.Modules.Category.Models.Category>(request._categoryRequest);

                newCategory.Id = request._categoryId;

                Models.Modules.Category.Models.Category category = _unitOfWork.CateogoryGenericRepository.Update(newCategory);
                _unitOfWork.SaveChanges();

                return _mapper.Map<CategoryResponse>(category);
            }
        }
    }
}