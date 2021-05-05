using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WindowsLiderEntrega.BusinessLogic.Interfaces;
using WindowsLiderEntrega.ServicioPrueba;
using WindowsLiderEntrega.Utility;

namespace WindowsLiderEntrega.BusinessLogic.Concrete
{
    public class TransactionBusiness : Abstract.BusinessAbract, ITransactionBusiness
    {
        private static object myLock = new object();
        private readonly List<Saldo> saldos = new List<Saldo>(); 

        /// <summary>
        /// Metodo que se encarga de obtener informacion del Servicio SErvicioPrueba y lanza multitareas.
        /// </summary>
        /// <returns></returns>
        public List<Saldo> ProcessTransactions()
        {
            ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };
            List<Transaccion> lclTransaction = new List<Transaccion>();
            using (ServiceClient client = new ServiceClient())
            {
                lclTransaction = ProxyWCF.GetDataTransactions(client, User, Password);
                List<Task> lstTask = new List<Task>();
                foreach (var transaccion in lclTransaction)
                {
                    Task baseTask = Task.Factory.StartNew(() => ProcessBalances(transaccion, client));
                    lstTask.Add(baseTask);
                }
                Task.WaitAll(lstTask.ToArray()); //Esperamos a que termine todo.
            }
            return saldos;
        }

        /// <summary>
        /// Metodo encargado de consumir el servicio para guardar el total de saldos.
        /// </summary>
        /// <param name="prmUser"></param>
        /// <param name="prmPassword"></param>
        /// <param name="prmBalances"></param>
        public void SaveData(List<Saldo> prmBalances) 
        {
            using (ServiceClient client = new ServiceClient())
            {
                ProxyWCF.SaveData(client, User, Password, saldos);
            }
        }


        /// <summary>
        /// Metodo que se encarga de calcular comisiones y agregar saldos. 
        /// </summary>
        /// <param name="transaccion"></param>
        /// <param name="serviceClient"></param>
        private void ProcessBalances(Transaccion transaccion, ServiceClient serviceClient)
        {
            var actualAcccount = transaccion.CuentaOrigen;
            var claveCifrado = ProxyWCF.GetEncryptedAccountKey(serviceClient, User, Password, actualAcccount);
            var movimientoActual = Encryptor.Decrypter(claveCifrado, transaccion.TipoTransaccion);
            double saldoActual = -1;
            //Obtenemos el saldo actual de la cuenta
            lock (myLock) //Aseguramos que nadie toque nuestra lista de saldos.
            {
                foreach (var saldo in saldos)
                {
                    if (actualAcccount == saldo.CuentaOrigen)
                    {
                        saldoActual = saldo.SaldoCuenta;

                        double comision = Calculator.CalculateCommission(Convert.ToInt64(transaccion.ValorTransaccion));

                        if (movimientoActual == "Debito")
                        {
                            saldo.SaldoCuenta -= transaccion.ValorTransaccion;
                        }
                        else
                        {
                            saldo.SaldoCuenta += transaccion.ValorTransaccion - comision;
                        }
                    }
                }
            }
            //Si no encuentra lo inserta
            if (saldoActual == -1)
            {
                Saldo saldo = new Saldo();
                double comision = Calculator.CalculateCommission(Convert.ToInt64(transaccion.ValorTransaccion));
                saldo.CuentaOrigen = actualAcccount;
                if (movimientoActual == "Debito")
                {
                    saldo.SaldoCuenta -= transaccion.ValorTransaccion;
                }
                else
                {
                    saldo.SaldoCuenta += transaccion.ValorTransaccion - comision;
                }
                lock (myLock)  //Aseguramos que nadie toque nuestra lista de saldos.
                {
                    saldos.Add(saldo);
                }
            }
        }


        public void CalculateProgressBar(ref System.Windows.Forms.ProgressBar progressBar) { 
        
        }
    }
}
