using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MydemoFirst.DataAccess.CacheService;

namespace MydemoFirst.Services.Application
{
    public class CacheBehavior<TRrequest, TRresponse> : IPipelineBehavior<TRrequest, TRresponse> where TRrequest : ICacheableRequest
    {

        private readonly ICacheService _cacheService;
        private readonly IMemoryCache _memoryCache;


        public CacheBehavior(ICacheService cacheService, IMemoryCache memoryCache)
        {

            _cacheService = cacheService;
            _memoryCache = memoryCache;



        }

        public async Task<TRresponse> Handle(TRrequest request, RequestHandlerDelegate<TRresponse> next, CancellationToken cancellationToken)
        {

            //return await _cacheService.GetOrCreate(
            //    request.KeyCache,
            //    async _ => await next(), request.Exp, cancellationToken

            //    );

            if (_cacheService.TryGetData<TRresponse>(request.KeyCache, out TRresponse cachedResponse))
            {
                return cachedResponse;
            }
            var response = await next();

            var data = _cacheService.SetData<TRresponse>(request.KeyCache, response, request.Exp);

            return response;




        }
    }
}
