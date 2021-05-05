using WindowsLiderEntrega.BusinessLogic.Concrete;
using WindowsLiderEntrega.BusinessLogic.Interfaces;

namespace WindowsLiderEntrega.BusinessLogic.Factory
{
    /// <summary>
    /// Fabrica para instanciar logica de negocio.
    /// </summary>
    public class FactoryBusiness
    {
        public static ITransactionBusiness CreateTransactionBusiness()
        {
            return new TransactionBusiness();
        }
    }
}
