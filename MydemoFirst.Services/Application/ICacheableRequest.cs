namespace MydemoFirst.Services.Application
{

    public interface ICacheableRequest
    {
        public string KeyCache { get; }

        public TimeSpan Exp { get; }
    }



}
