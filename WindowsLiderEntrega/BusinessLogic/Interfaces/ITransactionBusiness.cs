using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsLiderEntrega.ServicioPrueba;

namespace WindowsLiderEntrega.BusinessLogic.Interfaces
{
    public interface ITransactionBusiness
    {
        List<Saldo> ProcessTransactions();

        void SaveData(List<Saldo> prmBalances);
    }
}
