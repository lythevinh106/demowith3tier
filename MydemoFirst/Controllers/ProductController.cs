using AutoMapper;
using DTOShared.FetchData;
using DTOShared.Modules;
using DTOShared.Modules.Products.Request;
using DTOShared.Modules.Products.Response;
using DTOShared.Pagging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MydemoFirst.Models.Modules.Products.Models;
using MydemoFirst.Services.Application.Product.Command;
using MydemoFirst.Services.Application.Product.Commands;
using MydemoFirst.Services.Application.Product.Queries;

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericBaseController
    {
        public ProductController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        [Authorize]
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<ActionResult<ProductResponse>> CreateProduct(ProductRequest productRequest)
        {
            ProductResponse newProduct = await _imediator.Send(new CreateProductCommand(productRequest));
            return Ok(newProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(string id)
        {
            ProductResponse newProduct = await _imediator.Send(new DeleteProductCommand(Int32.Parse(id)));
            return Ok(newProduct);
        }

        //[Authorize]
        //[Authorize(Policy = "IsAdmin")]

        [HttpGet("allProduct")]
        public async Task<ActionResult<PaggingResponse<ProductResponse>>> GetAllProduct([FromQuery] FetchDataProductRequest fetchDataProductRequest)
        {
            PagedList<Product> dataProducts = await _imediator.Send(new FetchProductQuery(fetchDataProductRequest));


            var results = new PaggingResponse<ProductResponse>
            {
                CurrentPage = dataProducts.CurrentPage,
                TotalPage = dataProducts.TotalPages,
                hasNext = dataProducts.HasNextPage,
                hasPrv = dataProducts.HasPreviousPage,
                Data = dataProducts.Select(p => _mapper.Map<ProductResponse>(p)).ToList()
            };

            return Ok(results);
        }







        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(string id)
        {


            ProductResponse newProduct = await _imediator.Send(new GetProductByIdQuery(Int32.Parse(id)));
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        //[Authorize]
        //[Authorize(Policy = "UpdateProductPolicy")]
        public async Task<ActionResult<GenericResponse<ProductResponse>>> UpdateProduct(string id, ProductRequest productRequest)
        {
            ProductResponse newProduct = await _imediator.Send(new UpdateProductComnnand(Int32.Parse(id), productRequest));


            var result = new GenericResponse<ProductResponse>
            {
                Msg = "Update Thành Công",
                Values = newProduct
            };


            return Ok(newProduct);
        }
    }
}