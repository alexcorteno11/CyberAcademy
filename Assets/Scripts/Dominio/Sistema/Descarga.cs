using System;

namespace CyberAcademy.Dominio.Sistema
{
    public class Descarga
    {
        public string Nombre { get; private set; }
        public string Fuente { get; private set; }
        public string Pista { get; private set; }
        public bool ContieneMalware { get; private set; }
        public bool FueEscaneada { get; private set; }
        public DecisionDescarga Decision { get; private set; }

        public Descarga(string nombre, string fuente, string pista, bool contieneMalware)
        {
            if (nombre == null || nombre.Trim() == "")
            {
                throw new ArgumentException("La descarga necesita un nombre.", nameof(nombre));
            }

            if (fuente == null || fuente.Trim() == "")
            {
                throw new ArgumentException("La descarga necesita una fuente.", nameof(fuente));
            }

            if (pista == null || pista.Trim() == "")
            {
                throw new ArgumentException("La descarga necesita una pista.", nameof(pista));
            }

            Nombre = nombre;
            Fuente = fuente;
            Pista = pista;
            ContieneMalware = contieneMalware;

            FueEscaneada = false;
            Decision = DecisionDescarga.SinDecidir;
        }

        public void RegistrarEscaneo()
        {
            if (Decision == DecisionDescarga.SinDecidir)
            {
                FueEscaneada = true;
            }
            else
            {
                throw new InvalidOperationException(
                    "El escaneo debe hacerse antes de aceptar o rechazar la descarga.");
            }
        }

        public void Aceptar()
        {
            Decision = DecisionDescarga.Aceptada;
        }

        public void Rechazar()
        {
            Decision = DecisionDescarga.Rechazada;
        }
    }
}