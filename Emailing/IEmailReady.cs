namespace RoomAid.ServiceLayer.Emailing
{
    /// <summary>
    /// Interface guaranteeing that anyone who uses it has all the fields needed to create an email message 
    /// all emails are sent from roomaidnotifications@gmail
    /// </summary>
    public interface IEmailReady
    {
        //body of the message
        string Body { get; set; }
        //subject line of message
        string Subject { get; set; }
        //username of person
        string ToUsername { get; set; }
        //email of person 
        string ToEmail { get; set; }
    }
}
