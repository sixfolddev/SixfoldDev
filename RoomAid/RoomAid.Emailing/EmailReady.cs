namespace RoomAid.Emailing
{
    /// <summary>
    /// Interface guaranteeing that anyone who uses it has all the fields needed to create an email message 
    /// all emails are sent from roomaidnotifications@gmail
    /// </summary>
    public abstract class EmailReady
    {
        //body of the message
        protected readonly string _body;
        //subject line of message
        protected readonly string _subject;
        //username of person
        protected readonly string _toUsername;
        //email of person 
        protected readonly string _toEmail;

        protected EmailReady(string body, string subject, string toUsername, string toEmail)
        {
            _body = body;
            _subject = subject;
            _toUsername = toUsername;
            _toEmail = toEmail;
        }
    }
}
