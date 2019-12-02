namespace Emailing
{
    public interface IEmailReady
    {
        string Body { get; set; }
        string Subject { get; set; }
        string ToUsername { get; set; }
        string ToEmail { get; set; }
    }
}
