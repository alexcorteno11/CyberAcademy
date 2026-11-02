using System.Collections.Generic;

namespace CyberAcademy.Dominio.Sistema
{
    public class  Correo
    {
        public string Remitente { get; private set; }
        public string Asunto { get; private set; }
        public string Cuerpo { get; private set; }
        public bool EsFraudulento { get; private set; }

        public DecisionCorreo Decision { get; private set; } = DecisionCorreo.SinDecidir;

        private List<string> pistas;
        
        public Correo(string remitente, string asunto, string cuerpo, bool esFraudulento)
        {
            Remitente = remitente;
            Asunto = asunto;
            Cuerpo = cuerpo;
            EsFraudulento = esFraudulento;
            
            pistas = new List<string>();
        }

        public void AgregarPista(string pista)
        {
            pistas.Add(pista);
        }

        public IReadOnlyList<string> ObtenerPistas()
        {
            return pistas.AsReadOnly();
        }

        public void Clasificar(DecisionCorreo decision)
        {
            Decision = decision;
        }

        public bool EstaClasificadoCorrectamente()
        {
            if(EsFraudulento && Decision == DecisionCorreo.MarcadoPeligroso)
            {
                return true;
            }
            else if (!EsFraudulento && Decision == DecisionCorreo.MarcadoSeguro)
            {
                return true;
            }
            return false;
        }
    }
}