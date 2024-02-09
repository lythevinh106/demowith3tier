using DTOShared.FetchData;
using DTOShared.Modules;
using DTOShared.Modules.Products.Request;
using DTOShared.Modules.Products.Response;
using DTOShared.Pagging;

namespace demoBlazor.Services.Product
{
    public interface IProductClient
    {
        Task<ProductResponse> GetProductByID(int id);
        Task<GenericResponse<ProductResponse>> UpdateProduct(int id, ProductRequest productRequest);
        Task<ProductResponse> DeleteProductById(int id);
        Task<PaggingResponse<ProductResponse>> FetchProduct(FetchDataProductRequest fetchDataProductRequest);
    }


}
