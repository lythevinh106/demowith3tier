using AutoMapper;
using DTOShared.Modules.Category.Request;
using DTOShared.Modules.Category.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;

using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Category.Command
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        private readonly CategoryRequest _categoryRequest;

        public CreateCategoryCommand(CategoryRequest categoryRequest)
        {
            _categoryRequest = categoryRequest;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateCategoryCommand, CategoryResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                if (await _unitOfWork.CateogoryGenericRepository.CheckExist(c => c.Name == request._categoryRequest.Name))
                {
                    throw new ValidationException("Category was existed.");
                }

                var newCategory = _mapper.Map<Models.Modules.Category.Models.Category>(request._categoryRequest);

                Models.Modules.Category.Models.Category category = await _unitOfWork.CateogoryGenericRepository.Add(newCategory);

                _unitOfWork.SaveChanges();

                return _mapper.Map<CategoryResponse>(category);
            }
        }
    }
}