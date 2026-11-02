using System.Collections.Generic;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Ataques
{
    public abstract class Ataque
    {
        public string Nombre { get; private set; }
        public TipoAtaque Tipo { get; private set; }
        public int Dificultad { get; private set; }

        protected Ataque(string nombre, TipoAtaque tipo, int dificultad)
        {
            Nombre = nombre;
            Tipo = tipo;
            Dificultad = dificultad;
        }

        public abstract ResultadoAtaque Ejecutar(SistemaSimulado objetivo, IEnumerable<IDefensa> defensas);


        protected bool FueBloqueado(IEnumerable<IDefensa> defensas)
        {
            foreach(var item in defensas)
            {
                if (item.PuedeBloquear(Tipo))
                {
                    return true;
                }
            }
            return false;
            
        }
    }
}