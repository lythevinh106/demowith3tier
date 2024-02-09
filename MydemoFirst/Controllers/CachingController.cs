using AutoMapper;
using DTOShared.FetchData;
using DTOShared.Modules.Products.Response;
using DTOShared.Pagging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MydemoFirst.DataAccess.CacheService;


using MydemoFirst.Models.Modules.Products.Models;
using MydemoFirst.Services.Application.Product.Queries;

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachingController : GenericBaseController
    {
        private readonly ICacheService _cacheService;
        private readonly IMemoryCache _memoryCache;

        public CachingController(IMediator mediator, IMapper mapper, ICacheService cacheService, IMemoryCache memoryCache) : base(mediator, mapper)
        {
            _cacheService = cacheService;
            _memoryCache = memoryCache;
        }

        //VaryByHeader là mỗi request có mỗi cache riêng biêt : truyền ở key:  User-Agent  trong post man

        ///nếu bên trình duyệ t Cache-Control: max-age=10 > 5  nen sẽ k ghi dè dc, nếu  max-age=4 < 5 nen ghi dè dc,
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]

        [HttpGet("democache1")]
        public async Task<IActionResult> DemoCache1()
        {
            return Ok(DateTime.Now.ToString());
        }

        [HttpGet("democache2")]

        ///cacch theo query
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "PageSize", "PageNumber" })]
        public async Task<IActionResult> democache2([FromQuery] FetchDataProductRequest fetchDataProductRequest)
        {
            //PagedList<Product> dataProducts = await _imediator.Send(new FetchProductQuery(fetchDataProductRequest));

            //var results = new PaggingResponse<ProductResponse>
            //{
            //    CurrentPage = dataProducts.CurrentPage,
            //    TotalPage = dataProducts.TotalPages,
            //    hasNext = dataProducts.HasNextPage,
            //    hasPrv = dataProducts.HasPreviousPage,
            //    Data = dataProducts.Select(p => _mapper.Map<ProductResponse>(p)).ToList()
            //};

            return Ok(DateTime.Now.ToString());
        }

        [HttpGet("democache3")]
        [ResponseCache(CacheProfileName = "Cache10Second")]
        public async Task<IActionResult> DemoCache3()
        {
            return Ok(DateTime.Now.ToString());
        }

        [HttpGet("demoMemoryCache1")]
        public async Task<ActionResult<PaggingResponse<ProductResponse>>> MemoryCache1([FromQuery] FetchDataProductRequest fetchDataProductRequest)
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

        [HttpGet("demoMemoryCache2")]
        public async Task<ActionResult> MemoryCache2()
        {
            PagedList<Product> dataProducts = _cacheService.GetData<PagedList<Product>>("AllProduct");


            if (dataProducts != null)
            {
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

            return Ok(null);


        }
    }
}