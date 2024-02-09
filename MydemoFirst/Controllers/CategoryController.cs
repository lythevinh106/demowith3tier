using AutoMapper;
using DTOShared.FetchData;
using DTOShared.Modules;
using DTOShared.Modules.Category.Request;
using DTOShared.Modules.Category.Response;
using DTOShared.Modules.Products.Response;
using DTOShared.Pagging;
using MediatR;
using Microsoft.AspNetCore.Mvc;


using MydemoFirst.Models.Modules.Category.Models;
using MydemoFirst.Services.Application.Category.Command;
using MydemoFirst.Services.Application.Category.Queries;

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : GenericBaseController
    {
        public CategoryController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Policy = "CreateCategoryPolicy")]
        public async Task<ActionResult<ProductResponse>> CreateCategory(CategoryRequest categoryRequest)
        {
            CategoryResponse newProduct = await _imediator.Send(new CreateCategoryCommand(categoryRequest));
            return Ok(newProduct);
        }






        [HttpDelete("{id}")]
        //[Authorize]
        //[Authorize(Policy = "DeleteCategoryPolicy")]
        public async Task<ActionResult<CategoryResponse>> DeleteCategory(int id)
        {
            CategoryResponse newCategory = await _imediator.Send(new DeleteCategoryCommand(id));
            return Ok(newCategory);
        }



        [HttpGet("allCategory")]
        public async Task<ActionResult<PaggingResponse<CategoryResponse>>> GetAllCategory([FromQuery] FetchDataCategoryRequest fetchDataCategoryRequest)
        {
            PagedList<Category> dataCategories = await _imediator.Send(new FetchCategoryQuery(fetchDataCategoryRequest));
            //   await Task.Delay(1000);
            var results = new PaggingResponse<CategoryResponse>
            {
                CurrentPage = dataCategories.CurrentPage,
                TotalPage = dataCategories.TotalPages,
                hasNext = dataCategories.HasNextPage,
                hasPrv = dataCategories.HasPreviousPage,
                Data = dataCategories.Select(p => _mapper.Map<CategoryResponse>(p)).ToList()
            };

            return Ok(results);
        }



        [HttpGet("all")]
        public async Task<ActionResult<List<CategoryResponse>>> AllCategory([FromQuery] FetchDataCategoryRequest fetchDataCategoryRequest)
        {
            var results = await _imediator.Send(new GetAllCategoryQuery());
            //   await Task.Delay(1000);
            var newResults = results.Select(c => _mapper.Map<CategoryResponse>(c)).ToList();


            return Ok(newResults);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(int id)
        {

            CategoryResponse newCategory = await _imediator.Send(new GetCategoryByIdQuery(id));
            return Ok(newCategory);
        }


        [HttpPut("{id}")]
        //[Authorize]

        public async Task<ActionResult<GenericResponse<CategoryResponse>>> UpdateCategory(string id, CategoryRequest categoryRequest)
        {
            CategoryResponse newCategory = await _imediator.Send(new UpdateCategoryComnnand(int.Parse(id), categoryRequest));


            var result = new GenericResponse<CategoryResponse>
            {

                Msg = "update thanh congggggg luthevinh",

                Values = newCategory

            };
            return Ok(result);

        }
    }
}
