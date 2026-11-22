using System;
using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Defensas
{
    public class Antivirus : IDefensa
    {
        public string Nombre { get; private set; }
        public int Costo { get; private set; }

        public Antivirus(int costo)
        {
            if (costo < 0)
            {
                throw new ArgumentException("El costo debe ser cero o mayor.", nameof(costo));
            }

            Nombre = "Antivirus";
            Costo = costo;
        }

        public bool PuedeBloquear(TipoAtaque tipoAtaque)
        {
            return false;
        }

        public bool Escanear(Descarga descarga)
        {
            if (descarga == null)
            {
                throw new ArgumentNullException(nameof(descarga));
            }

            descarga.RegistrarEscaneo();
            return descarga.ContieneMalware;
        }
    }
}