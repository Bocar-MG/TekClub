namespace TekClub.Models.Emails
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
