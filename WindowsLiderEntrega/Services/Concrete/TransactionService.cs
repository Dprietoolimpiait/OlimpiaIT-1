using System.Collections.Generic;
using System.Linq;
using WindowsLiderEntrega.BusinessLogic.Factory;
using WindowsLiderEntrega.BusinessLogic.Interfaces;
using WindowsLiderEntrega.Services.Interfaces;
using WindowsLiderEntrega.ServicioPrueba;

namespace WindowsLiderEntrega.Services.Concrete
{
    /// <summary>
    /// Clase facade hacia capa business
    /// </summary>
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// Metodo que consume servicio para almacenar el total de saldos calculados.
        /// </summary>
        /// <param name="prmBalances"></param>
        public void SaveData(List<Saldo> prmBalances)
        {
            ITransactionBusiness transactionBusiness = FactoryBusiness.CreateTransactionBusiness();
            transactionBusiness.SaveData(prmBalances);
        }

        /// <summary>
        /// Metodo que orquesta todo el procesamiento de transacciones y saldos.
        /// </summary>
        /// <returns></returns>
        public List<Saldo> ProcessTransactions() 
        {
            ITransactionBusiness transactionBusiness = FactoryBusiness.CreateTransactionBusiness();
            return transactionBusiness.ProcessTransactions();
        }
    }
}
