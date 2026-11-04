using CyberAcademy.Dominio.Juego;
using UnityEngine;
using UnityEngine.UI;

namespace CyberAcademy.UI
{
    public class EscritorioUI : MonoBehaviour
    {
        [SerializeField] private Button iconoCorreo;
        [SerializeField] private VentanaUI ventanaCorreo;
        [SerializeField] private PanelMisionUI panelMision;
        [SerializeField] private ControladorNivelBase controladorNivel1;

        private Campana campana;

       private void Start()
        {
            campana = CreadorDeCampana.CrearCampanaBase();
            var nivel = campana.NivelActual;
            panelMision.Mostrar(nivel.Mision);
            controladorNivel1.Iniciar(nivel);
            iconoCorreo.onClick.AddListener(() => ventanaCorreo.Abrir());
            controladorNivel1.NivelTerminado += AlTerminarNivel;
        }

        private void AlTerminarNivel()
        {
            bool nivelDesbloqueado = campana.nuevoDesbloqueo();
            ventanaCorreo.Cerrar();
            campana.ReiniciarNivelActual();
            var nivel = campana.NivelActual;
            panelMision.Mostrar(nivel.Mision);
            controladorNivel1.Iniciar(nivel);
            Debug.Log($"Nivel superado: {nivelDesbloqueado}");

        }

        private void OnDestroy()
        {
            iconoCorreo.onClick.RemoveAllListeners();
            controladorNivel1.NivelTerminado -= AlTerminarNivel;
        }
    }
}