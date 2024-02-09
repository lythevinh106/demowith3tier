using DTOShared.FetchData;
using DTOShared.Modules;
using DTOShared.Modules.Category.Request;
using DTOShared.Modules.Category.Response;
using DTOShared.Pagging;

namespace demoBlazor.Services.Cateogory
{
    public interface ICateogryClient
    {
        Task<CategoryResponse> GetCategoryByID(int id);
        Task<GenericResponse<CategoryResponse>> UpdateCategory(int id, CategoryRequest categoryRequest);
        Task<CategoryResponse> DeleteCategoryById(int id);
        Task<PaggingResponse<CategoryResponse>> FetchCategory(FetchDataCategoryRequest fetchDataCategoryRequest);
        Task<List<CategoryResponse>> All();
    }
}
