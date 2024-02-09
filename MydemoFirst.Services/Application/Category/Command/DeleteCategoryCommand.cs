using AutoMapper;
using DTOShared.Modules.Category.Response;
using MediatR;
using MydemoFirst.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.Application.Category.Command
{
    public class DeleteCategoryCommand : IRequest<CategoryResponse>
    {
        private readonly int _categoryId;

        public DeleteCategoryCommand(int categoryId)
        {
            _categoryId = categoryId;
        }

        public class Handler : BaseHandler, IRequestHandler<DeleteCategoryCommand, CategoryResponse>
        {
            public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public async Task<CategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {



                if (!await _unitOfWork.CateogoryGenericRepository.CheckExist(c => c.Id == request._categoryId))
                {
                    throw new ValidationException("Category is not exist .");
                }

                Models.Modules.Category.Models.Category category = await _unitOfWork.CateogoryGenericRepository.Get(request._categoryId);

                Models.Modules.Category.Models.Category categoryRemove = _unitOfWork.CateogoryGenericRepository.Delete(category);

                _unitOfWork.SaveChanges();
                return _mapper.Map<CategoryResponse>(categoryRemove);
            }
        }
    }
}