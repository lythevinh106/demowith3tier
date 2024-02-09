namespace MydemoFirst.DataAccess.CacheService
{
    public interface ICacheService
    {
        public Task<T> GetOrCreate<T>(
            string key, Func<CancellationToken, Task<T>> factory,

            TimeSpan? expritation = null, CancellationToken cancellation = default);

        public bool RemoveData(string key);

        public T GetData<T>(string key);

        public bool SetData<T>(string key, T value, TimeSpan exp);




        public bool TryGetData<T>(string key, out T cacheT);





    }
}
