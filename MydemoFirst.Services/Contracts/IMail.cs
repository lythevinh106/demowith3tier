namespace MydemoFirst.Services.Contracts
{
    public interface IMail
    {
        Task<bool> SendMailAsync(string subject, string body, string emailTo);

        Task<bool> SendMailAsyncHtml(string subject, string contentHtml, string emailTo);

        Task Disconect();
    }
}
