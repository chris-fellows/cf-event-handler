//using CFEventHandler.Email;
//using CFEventHandler.JSON;

//namespace CFEventHandler.Common.Email
//{
//    public class JSONEmailTemplateService : JSONItemRepository<EmailTemplate, string>, IEmailTemplateService
//    {
//        public JSONEmailTemplateService(string folder) :
//                       base(folder,
//                           ((EmailTemplate emailTemplate) => { return emailTemplate.Id; }),
//                           ((EmailTemplate emailTemplate) => { emailTemplate.Id = Guid.NewGuid().ToString(); }))
//        {

//        }
//    }
//}
