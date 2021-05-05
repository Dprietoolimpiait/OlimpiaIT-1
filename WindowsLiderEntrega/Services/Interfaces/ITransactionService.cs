using System.Collections.Generic;
using WindowsLiderEntrega.ServicioPrueba;

namespace WindowsLiderEntrega.Services.Interfaces
{
    public interface ITransactionService
    {
        void SaveData(List<Saldo> prmBalances);
        List<Saldo> ProcessTransactions();
    }
}
