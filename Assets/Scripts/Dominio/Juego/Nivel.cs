using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;
using System.Collections.Generic;

namespace CyberAcademy.Dominio.Juego
{
    public class Nivel
    {
        public string Nombre { get; private set; }
        public SistemaSimulado Sistema { get; private set; }
        public Ataque AtaqueNivel { get; private set; }
        public int VidaMinimaParaGanar { get; private set; }
        public int ReputacionMinimaParaGanar { get; private set; }
        public FaseNivel Fase { get; private set; } = FaseNivel.Preparacion;
        public ResultadoAtaque ResultadoFinal { get; private set; }
        private List<IDefensa> defensasActivas;
        public string Mision { get; private set; }

        public IReadOnlyList<IDefensa> ObtenerDefensasActivas()
        {
            return defensasActivas.AsReadOnly();
        }

        public Nivel (string nombre, SistemaSimulado sistema, Ataque ataque, int vidaMinimaParaGanar, int reputacionMinimaParaGanar, string mision)
        {
            Nombre = nombre;
            Sistema = sistema;
            AtaqueNivel = ataque;
            VidaMinimaParaGanar = vidaMinimaParaGanar;
            ReputacionMinimaParaGanar = reputacionMinimaParaGanar;
            defensasActivas = new List<IDefensa>();
            Mision = mision;
        }

        public bool AgregarDefensa(IDefensa defensa)
        {
            if (Fase != FaseNivel.Preparacion)
            {
                return false;
            }
            defensasActivas.Add(defensa);
            return true;
        }

        public ResultadoAtaque EjecutarAtaque()
        {
            if(Fase != FaseNivel.Preparacion)
            {
                throw new System.InvalidOperationException("El ataque ya fue ejecutado");
            }
            Fase = FaseNivel.Ataque;
            ResultadoFinal = AtaqueNivel.Ejecutar(Sistema, defensasActivas);

            Fase = FaseNivel.Resultado;
            return ResultadoFinal;
        }

        public bool Superado()
        {
            if (Fase == FaseNivel.Resultado && Sistema.CalcularVidaSistema() >= VidaMinimaParaGanar && Sistema.Reputacion >= ReputacionMinimaParaGanar)
            {
                return true;
            }
            return false;
        }

        
    }

}
