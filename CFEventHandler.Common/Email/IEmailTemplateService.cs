namespace CFEventHandler.Common.Email
{
    public interface IEmailTemplateService
    {
        IEnumerable<EmailTemplate> GetAll();
    }
}
