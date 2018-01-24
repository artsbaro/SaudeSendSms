using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace SaudeServiceEnvioSms
{
    partial class SendSms : ServiceBase
    {
        public SendSms()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Initialize();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        public void InitializeSend()
        {
            while (true)
            {
                int time = 600000;
                var prm = ConfigurationManager.AppSettings["IntervaloDeTempoParaExecucao"];
                if (prm?.Length > 0)
                    time = Convert.ToInt32(prm);

                Thread.Sleep(time);
                Process();
            }
        }

        public void Initialize()
        {
            ThreadStart start = new ThreadStart(InitializeSend);
            Thread thread = new Thread(start);

            thread.Start();
        }

        private void SendSmsMethod()
        {
            double timeInHours = 12;
            var prm = ConfigurationManager.AppSettings["TempoEmHorasAnteriorAConsultaParaEnvioDeSms"];
            if (prm?.Length > 0)
                timeInHours = Convert.ToDouble(prm);

            var dateCut = DateTime.Now.AddHours(timeInHours);

            /*
              1 - Montar Lista de Consultas Médicas com Data da Consulta (PATC_D_DATA) inferior a dateCut
               1.1 Consulta Não efetuada
               1.2 Paciente existente e ativo
               1.3 Telefone do paciente Válido

              2 -  Montar Json com os Sms a serem enviados
               
              3 - Enviar SMS  

             */
        }

        private void Process()
        {
            DateTime horaInicialExecucao = DateTime.MinValue;
            var prm = ConfigurationManager.AppSettings["ExecucaoPeriodoInicial"];
            if (prm?.Length > 0)
                horaInicialExecucao = Convert.ToDateTime($"{DateTime.Today:yyyy-MM-dd}" + " " + prm);

            DateTime horaFinalExecucao = DateTime.MinValue;
            prm = ConfigurationManager.AppSettings["ExecucaoPeriodoFinal"];
            if (prm?.Length > 0)
                horaFinalExecucao = Convert.ToDateTime($"{DateTime.Today:yyyy-MM-dd}" + " " + prm);

            if (DateTime.Now >= horaInicialExecucao && DateTime.Now <= horaFinalExecucao)
            {
                SendSmsMethod();
            }
        }
    }
}
