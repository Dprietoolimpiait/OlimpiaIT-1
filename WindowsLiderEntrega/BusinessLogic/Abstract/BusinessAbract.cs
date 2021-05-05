using System.Configuration;

namespace WindowsLiderEntrega.BusinessLogic.Abstract
{
    public abstract class BusinessAbract
    {
        //Constr.
        public BusinessAbract()
        {
            User = ConfigurationManager.AppSettings["User"];
            Password = ConfigurationManager.AppSettings["Password"];
        }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
