using Fluxor;

namespace demoBlazor.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        public void AfterDispatch(object action)
        {
            throw new NotImplementedException();
        }

        public void AfterInitializeAllMiddlewares()
        {
            throw new NotImplementedException();
        }

        public void BeforeDispatch(object action)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginInternalMiddlewareChange()
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync(IDispatcher dispatcher, IStore store)
        {
            throw new NotImplementedException();
        }

        public bool MayDispatchAction(object action)
        {
            throw new NotImplementedException();
        }
    }
}
