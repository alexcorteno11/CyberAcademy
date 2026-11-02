using System.Collections.Generic;
using System.Linq.Expressions;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Ataques
{
    public class Phishing : Ataque
    {
        public Phishing(int dificultad) : base("Phishing", TipoAtaque.Phishing, dificultad)
        {
        }

        public override ResultadoAtaque Ejecutar(SistemaSimulado objetivo, IEnumerable<IDefensa> defensas)
        {
            if (FueBloqueado(defensas))
            {
                return new ResultadoAtaque(true, 0, "El ataque de Phishing fue bloqueado por las defensas.");

            }
            int fraudesNoDetectados = 0;
            int falsosPositivos = 0;

            foreach (var item in objetivo.ObtenerCorreos())
            {

                if (item.EsFraudulento && !item.EstaClasificadoCorrectamente())
                {
                    fraudesNoDetectados++;
                }
                else if (!item.EsFraudulento && item.Decision == DecisionCorreo.MarcadoPeligroso)
                {
                    falsosPositivos++;
                }
            }

            int danio = 0;

            for (int i = 0; i < fraudesNoDetectados; i++)
            {
                foreach (var item in objetivo.ObtenerArchivos())
                {
                    if (item.Estado == EstadoArchivo.Sano)
                    {
                        item.Infectar();
                        danio++;
                        break;
                    }
                }
            }

            objetivo.PerderReputacion(falsosPositivos * 15);
            bool bloqueado = fraudesNoDetectados == 0;

            if (bloqueado && falsosPositivos == 0)
            {
                return new ResultadoAtaque(bloqueado, danio, "Se clasificaron todos los correos correctamente.");
            }

           
            return new ResultadoAtaque(bloqueado, danio, $"El ataque de Phishing causó {danio} archivos infectados. Fraudes no detectados: {fraudesNoDetectados}, Falsos positivos: {falsosPositivos} ");

        } 

            
    }
}
