using BlazorBootstrap;
using demoBlazor.FluxorServices.Error;
using demoBlazor.FluxorServices.Other;
using demoBlazor.Helpers;
using DTOShared.FetchData;
using DTOShared.Modules;
using DTOShared.Modules.Category.Request;
using DTOShared.Modules.Category.Response;
using DTOShared.Pagging;
using Fluxor;
using System.Net.Http.Json;

namespace demoBlazor.Services.Cateogory
{
    public class CategoryClient : ICateogryClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDispatcher _dispatcher;
        private readonly ToastService _toastService;











        public CategoryClient(HttpClient httpClient, IDispatcher dispatcher, ToastService toastService





            )
        {
            _dispatcher = dispatcher;
            _httpClient = httpClient;
            _toastService = toastService;



        }

        public async Task<List<CategoryResponse>> All()
        {
            try
            {
                _dispatcher.Dispatch(new ActiveLoading(true));


                var response =
                    await _httpClient.GetFromJsonAsync<List<CategoryResponse>>($"/api/Category/all");


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

        public Task<CategoryResponse> DeleteCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaggingResponse<CategoryResponse>> FetchCategory(FetchDataCategoryRequest fetchDataCategoryRequest)
        {



            try
            {


                //_dispatcher.Dispatch(new ActiveLoading(true));


                string query = StringHelper.ConvertObjectToSearchParam(fetchDataCategoryRequest);
                Console.WriteLine("query-------" + query);

                PaggingResponse<CategoryResponse> response =
                    await _httpClient.GetFromJsonAsync<PaggingResponse<CategoryResponse>>($"/api/Category/allCategory?{query}");


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

        public async Task<CategoryResponse> GetCategoryByID(int id)
        {


            try
            {
                _dispatcher.Dispatch(new ActiveLoading(true));
                CategoryResponse response = await _httpClient.GetFromJsonAsync<CategoryResponse>($"/pi/Category/{id}");
                await Task.Delay(2000);
                _dispatcher.Dispatch(new ActiveLoading(false));

                return response;

            }
            catch (Exception ex)
            {

                _dispatcher.Dispatch(new ActiveLoading(false));

                _dispatcher.Dispatch(new ActiveError(true, ex.Message));


                throw new Exception(ex.Message);

            }





        }



        public async Task<GenericResponse<CategoryResponse>> UpdateCategory(int id, CategoryRequest categoryRequest)
        {
            try
            {
                _dispatcher.Dispatch(new ActiveLoading(true));

                var response =
                    await _httpClient.PutAsJsonAsync($"/api/Category/{id}", categoryRequest);

                if (response.IsSuccessStatusCode)
                {
                    GenericResponse<CategoryResponse> data = await response.Content.ReadFromJsonAsync<GenericResponse<CategoryResponse>>();

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
