using Hangfire;
using MediatR;
using MydemoFirst.Services.Contracts;

namespace MydemoFirst.Services.Notification
{
    public class SendMailNotification : INotification
    {
        public string subject { get; set; }
        public string body { get; set; }
        public string emailTo { get; set; }

        public SendMailNotification(string subject, string body, string mailTo)
        {
            this.subject = subject;
            this.body = body;
            this.emailTo = mailTo;
        }


    }



    public class SendMailNotificationHandler : INotificationHandler<SendMailNotification>
    {


        public Task Handle(SendMailNotification notification, CancellationToken cancellationToken)
        {

            string jobId = BackgroundJob.Enqueue<IMail>(x => x.SendMailAsync(notification.subject, notification.body, notification.emailTo));

            return Task.CompletedTask;
        }
    }
}
