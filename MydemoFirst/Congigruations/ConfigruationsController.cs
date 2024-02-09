using Microsoft.AspNetCore.Mvc;
using MydemoFirst.Filter;
using System.Text.Json.Serialization;

namespace MydemoFirst.Congigruations
{
    public static class ConfigruationsController
    {



        public static void ConfigueController(this IServiceCollection service)
        {
            service.AddControllers(options =>
            {

                // implement filters cacche global
                //options.Filters.Add(
                //  new ResponseCacheAttribute
                //  {
                //      Duration = 10,

                //  });

                //cache profile

                options.CacheProfiles.Add("Cache10Second",
                new CacheProfile()
                {
                    Duration = 10
                });

                // implement filters

                options.Filters.Add<ResponseModelCustomFilter>();





            }).AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                x.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        }



    }
}
