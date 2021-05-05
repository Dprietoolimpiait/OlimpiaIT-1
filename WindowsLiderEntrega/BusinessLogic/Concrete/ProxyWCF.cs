using System.Collections.Generic;
using System.Linq;
using WindowsLiderEntrega.ServicioPrueba;

namespace WindowsLiderEntrega.BusinessLogic.Concrete
{
    /// <summary>
    /// Operaciones de WCF ServicioPrueba
    /// </summary>
    public class ProxyWCF
    {
        public static List<Transaccion> GetDataTransactions(ServiceClient prmClient, string prmUser, string prmPassword)
        {
            return prmClient.GetData(prmUser, prmPassword).ToList();
        }

        public static string GetEncryptedAccountKey(ServiceClient prmClient, string prmUser, string prmPassword, long prmActualAccount)
        {
            return prmClient.GetClaveCifradoCuenta(prmUser, prmPassword, prmActualAccount);
        }

        public static void SaveData(ServiceClient prmClient, string prmUser, string prmPassword, List<Saldo> prmBalances)
        {
            prmClient.SaveData(prmUser, prmPassword, prmBalances.ToArray());
        }
    }
}
