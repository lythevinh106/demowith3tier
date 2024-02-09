using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MydemoFirst.DataAccess.CacheService;

namespace MydemoFirst.Services.Application
{
    public class UpdateCacheBehavior<TRrequest, TRresponse> : IPipelineBehavior<TRrequest, TRresponse>
        where TRrequest : IRemoveableCache

    {

        private readonly ICacheService _cacheService;
        private readonly IMemoryCache _memoryCache;


        public UpdateCacheBehavior(ICacheService cacheService, IMemoryCache memoryCache)
        {

            _cacheService = cacheService;
            _memoryCache = memoryCache;



        }

        public async Task<TRresponse> Handle(TRrequest request, RequestHandlerDelegate<TRresponse> next, CancellationToken cancellationToken)
        {




            var response = await next();

            if (_cacheService.TryGetData<object>(request.KeyCache, out object cachedResponse))
            {
                _cacheService.RemoveData(request.KeyCache);
            }

            return response;




        }
    }
}
