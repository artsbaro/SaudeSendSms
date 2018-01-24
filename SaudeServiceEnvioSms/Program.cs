using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SaudeServiceEnvioSms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {





            // Não é necessário instalar o Windows Service para testes!
            // Bastar setar a solution em modo Debug
            // E configurar os dois projetos para inicializarem juntos

#if (!DEBUG)
                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[] 
                        { 
                            new SendSms() 
                        };
                        ServiceBase.Run(ServicesToRun);
#else
            var service = new SendSms();
            // Chamada do seu método para Debug.
            service.Initialize();
            // Coloque sempre um breakpoint para o ponto de parada do seu código.
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif









            //Outro Servico aqui
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new SendSms(),
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
