namespace MydemoFirst.Services.Contracts
{
    public interface IServiceBus<T>
    {
        Task Send(string topicName, T entity, Dictionary<string, string> filters = null);

        Task<T> Recevie(string topicName);



    }
}
