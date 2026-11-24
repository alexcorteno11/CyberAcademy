
using System;
using CyberAcademy.Dominio.Ataques;

namespace CyberAcademy.Dominio.Defensas
{
    public class Firewall : IDefensa, ILimitadorDeIntentos
    {
        public string Nombre { get; private set; }
        public int Costo { get; private set; }
        public int LimiteIntentos { get; private set; }

        public Firewall(int limiteIntentos)
        {
            if (limiteIntentos < 1)
            {
                throw new ArgumentException(
                    "El límite debe ser mayor que cero.");
            }

            Nombre = "Firewall";
            Costo = 20;
            LimiteIntentos = limiteIntentos;
        }

        public bool PuedeBloquear(TipoAtaque tipoAtaque)
        {
            return false;
        }
    }
}

