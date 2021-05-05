//PRUEBA OLIMPIA
//La función de la aplicación actual es calcular el saldo final de las cuentas de un "banco", para esto se consume un servicio que devuelve 
//las transacciones realizas a la cuentas.

//Paso 1: Hacer funcionar la aplicación. Debido al aumento de transacciones y al  colocar al servicio con SSL la aplicación actual esta fallando.
//Paso 2: Estructurar mejor el codigo. Uso de patrones, buenas practicas, etc.
//Paso 3: Optimizar el codigo, como se menciono en el paso 1 el aumento de transacciones ha causado que el calculo de los saldos se demore demasiado.
//Paso 4: Adicionar una barra de progreso al formulario. Actualizar la barra con el progreso del proceso, evitando bloqueos del GUI.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsLiderEntrega.Exceptions;
using WindowsLiderEntrega.Services.Interfaces;
using WindowsLiderEntrega.ServicioPrueba;

namespace WindowsLiderEntrega
{
    public partial class frmTransaction : Form
    {
        private readonly ITransactionService _service;
        public frmTransaction(ITransactionService service)
        {
            _service = service;
            InitializeComponent();
        }

        //Evento
        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                lblTiempoTotal.Text = string.Empty;
                EnabledBtnCalculate(false);
                pbrProcessTransaction.Value = 0;
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
                TimerPbrTransaction.Enabled = true;
                await Task.Factory.StartNew(() =>
                    {
                        List<Saldo> lclSaldos = _service.ProcessTransactions();
                        if (lclSaldos != null && lclSaldos.Any())
                        {
                            _service.SaveData(lclSaldos);
                        }
                    });
                lblTiempoTotal.Text = stopwatch.ElapsedMilliseconds.ToString();
                EnabledBtnCalculate(true);
                stopwatch.Stop();
            }
            catch (Exception ex) {
                LogException.WriteLog(ex, "Transaction.ProcessTransactions", LogType.Error);
                lblTiempoTotal.Text = "Ha ocurrido un error, por favor ver log \n de errores en " + ConfigurationManager.AppSettings["PathExceptions"].ToString();
            }
        }

        private void EnabledBtnCalculate(bool prmEnabled) {
            btnCalculate.Enabled = prmEnabled;
        }

        private void TimerPbrTransaction_Tick(object sender, EventArgs e)
        {
            if (pbrProcessTransaction.Value != pbrProcessTransaction.Maximum)
            {
                if (lblTiempoTotal.Text == string.Empty)
                {
                    pbrProcessTransaction.Value++;
                }
                else {
                    pbrProcessTransaction.Value = pbrProcessTransaction.Maximum;
                }
            }
            else
            {
                TimerPbrTransaction.Stop();
            }
        }
    }
}
