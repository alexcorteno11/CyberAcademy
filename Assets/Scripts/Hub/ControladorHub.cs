using UnityEngine;
using CyberAcademy.Dominio.Juego;
using CyberAcademy.UI;
using TMPro;

namespace CyberAcademy.Hub
{
    public class ControladorHub : MonoBehaviour
    {
        [SerializeField] private ComputadoraInteractuable[] computadoras;
        [SerializeField] private MovimientoJugador jugador;
        [SerializeField] private EscritorioUI escritorio;
        [SerializeField] private TMP_Text textoObjetivo;

        private Campana campana;
        private bool escritorioAbierto;

        private void Start()
        {
            campana = CreadorDeCampana.CrearCampanaBase();
            foreach (var computadora in computadoras)
            {
                computadora.InteraccionSolicitada += AlSolicitarInteraccion;
            }
            escritorio.Cerrado += AlCerrarEscritorio;
            ActualizarComputadoras();
        }
        private void OnDestroy()
        {
            foreach (var computadora in computadoras)
            {
                computadora.InteraccionSolicitada -= AlSolicitarInteraccion;
            }
            escritorio.Cerrado -= AlCerrarEscritorio;
        }

        private void AlSolicitarInteraccion(int indiceNivel)
        {
            if (escritorioAbierto)
            {
                return;
            }
            escritorioAbierto = true;
            jugador.ActivarMovimiento(false);
            escritorio.Abrir(campana, indiceNivel);

        }
        private void AlCerrarEscritorio()
        {
            escritorioAbierto = false;
            jugador.ActivarMovimiento(true);
            ActualizarComputadoras();

        }
        private void ActualizarComputadoras()
        {
            foreach (var c in computadoras)
            {
                c.ActualizarEstado(ObtenerEstado(c.IndiceNivel));
            }
            if(campana.EstaCompletada)
            {
                textoObjetivo.text = "¡Felicidades! Has completado la campaña";
            }
            else
            {
                textoObjetivo.text = $"Objetivo: Completar el nivel {campana.NivelMaximoDesbloqueado + 1}";
            }
        }
        private EstadoComputadora ObtenerEstado(int indiceNivel)
        {
            if (indiceNivel < campana.NivelMaximoDesbloqueado || (indiceNivel == campana.TotalNiveles - 1 && campana.EstaCompletada))
            {
                return EstadoComputadora.Completada;
            }
            else if (campana.EstaDesbloqueado(indiceNivel))
            {
                return EstadoComputadora.Disponible;

            }
            else
            {
                return EstadoComputadora.Bloqueada;

            }
        }
    }
}
