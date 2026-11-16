using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;
using System.Collections.Generic;
using System;

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
        public AccionesTurno Acciones { get; private set; }
        public bool EsPorTurnos => AtaqueNivel is IAtaquePorPasos;
        public int TurnoActual => (AtaqueNivel as IAtaquePorPasos)?.TurnoActual ?? 0;
        public int TurnosTotales => (AtaqueNivel as IAtaquePorPasos)?.TurnosTotales ?? 0;

        public IReadOnlyList<IDefensa> ObtenerDefensasActivas()
        {
            return defensasActivas.AsReadOnly();
        }

        public Nivel (string nombre, SistemaSimulado sistema, Ataque ataque, int vidaMinimaParaGanar, int reputacionMinimaParaGanar, string mision, AccionesTurno acciones = null)
        {
            Nombre = nombre;
            Sistema = sistema;
            AtaqueNivel = ataque;
            VidaMinimaParaGanar = vidaMinimaParaGanar;
            ReputacionMinimaParaGanar = reputacionMinimaParaGanar;
            defensasActivas = new List<IDefensa>();
            Mision = mision;
            Acciones = acciones;
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

        public void IniciarAtaquePorTurnos()
        {
            if (Fase != FaseNivel.Preparacion)
            {
                throw new InvalidOperationException("El nivel no está en la fase de preparación");
            }
            else if (!EsPorTurnos)
            {
                throw new InvalidOperationException("El ataque no es por turnos");
            }
            else
            {
                Fase = FaseNivel.Ataque;
            }
            
        }
        
        public ResultadoAtaque AvanzarTurno()
        {
            if(Fase != FaseNivel.Ataque)
            {
                throw new InvalidOperationException("El ataque no está en curso o no es por turnos");
            }
            var ataquePorTurnos = AtaqueNivel as IAtaquePorPasos;
            var resultado = ataquePorTurnos.EjecutarPaso(Sistema, defensasActivas);
            if (Acciones != null)
            {
                Acciones.ReiniciarPuntos();
            }
            
            if (ataquePorTurnos.HaTerminado)
            {
                Fase = FaseNivel.Resultado;
                ResultadoFinal = resultado;
                
            }
            return resultado;
        }

        public bool Respaldar(Archivo archivo)
        {
            if(Fase == FaseNivel.Resultado || Acciones == null)
            {
                return false;
            }
            
            return Acciones.Respaldar(archivo);
        }

        public bool Restaurar(Archivo archivo)
        {
            if (Fase != FaseNivel.Ataque || Acciones == null)
            {
                return false;
            }
            return Acciones.Restaurar(archivo);
        }


    }

}
