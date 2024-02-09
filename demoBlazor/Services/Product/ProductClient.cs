using BlazorBootstrap;
using demoBlazor.FluxorServices.Other;
using demoBlazor.Helpers;
using DTOShared.FetchData;
using DTOShared.Modules;
using DTOShared.Modules.Products.Request;
using DTOShared.Modules.Products.Response;
using DTOShared.Pagging;
using Fluxor;
using System.Net.Http.Json;

namespace demoBlazor.Services.Product
{
    public class ProductClient : IProductClient
    {

        private readonly HttpClient _httpClient;
        private readonly IDispatcher _dispatcher;
        private readonly ToastService _toastService;
        public ProductClient(HttpClient httpClient, IDispatcher dispatcher, ToastService toastService)
        {
            _dispatcher = dispatcher;
            _httpClient = httpClient;
            _toastService = toastService;

        }


        public Task<ProductResponse> DeleteProductById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaggingResponse<ProductResponse>> FetchProduct(FetchDataProductRequest fetchDataProductRequest)
        {
            try
            {
                _dispatcher.Dispatch(new ActiveLoading(true));
                string query = StringHelper.ConvertObjectToSearchParam(fetchDataProductRequest);
                Console.WriteLine("query-------" + query);

                PaggingResponse<ProductResponse> response =
                    await _httpClient.GetFromJsonAsync<PaggingResponse<ProductResponse>>($"/api/Product/allProduct?{query}");


                //await Task.Delay(4000);
                _dispatcher.Dispatch(new ActiveLoading(false));

                return response;

            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new ActiveLoading(false));
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductResponse> GetProductByID(int id)
        {
            try
            {
                _dispatcher.Dispatch(new ActiveLoading(true));
                ProductResponse response = await _httpClient.GetFromJsonAsync<ProductResponse>($"/api/Product/{id}");
                await Task.Delay(2000);
                _dispatcher.Dispatch(new ActiveLoading(false));

                return response;

            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new ActiveLoading(false));
                throw new Exception(ex.Message);
            }
        }

        public async Task<GenericResponse<ProductResponse>> UpdateProduct(int id, ProductRequest productRequest)
        {
            try
            {
                _dispatcher.Dispatch(new ActiveLoading(true));
                var response =
                    await _httpClient.PutAsJsonAsync($"/api/Product/{id}", productRequest);

                if (response.IsSuccessStatusCode)
                {
                    GenericResponse<ProductResponse> data = await response.Content.ReadFromJsonAsync<GenericResponse<ProductResponse>>();


                    _dispatcher.Dispatch(new ActiveLoading(false));

                    return data;

                }
                return null;


            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new ActiveLoading(false));

                _toastService.Notify(new(ToastType.Warning, $"Thong bao", "Update that bai"));
                throw new Exception(ex.Message);
            }
        }
    }
}
