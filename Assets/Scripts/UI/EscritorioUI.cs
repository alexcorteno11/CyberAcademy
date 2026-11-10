using CyberAcademy.Dominio.Juego;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CyberAcademy.UI
{
    public class EscritorioUI : MonoBehaviour
    {
        [SerializeField] private GameObject canvasEscritorio;
        [SerializeField] private Button iconoCorreo;
        [SerializeField] private VentanaUI ventanaCorreo;
        [SerializeField] private PanelMisionUI panelMision;
        [SerializeField] private ControladorNivelBase controladorNivel1;

        public event Action Cerrado;

        private Campana campana;

       private void Start()
        {
            iconoCorreo.onClick.AddListener(() => ventanaCorreo.Abrir());
            controladorNivel1.NivelTerminado += AlTerminarNivel;
            controladorNivel1.ReintentoSolicitado += AlReintentar;
            canvasEscritorio.SetActive(false);
        }
        public void Update()
        {
            if (canvasEscritorio.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                Cerrar();
            }
        }

        public void Abrir(Campana campanaRecibida, int indiceNivel)
        {
            this.campana = campanaRecibida;
            campana.SeleccionarNivel(indiceNivel);
            canvasEscritorio.SetActive(true);
            IniciarNivelActual();
        }

        public void Cerrar()
        {
            ventanaCorreo.Cerrar();
            canvasEscritorio.SetActive(false);
            Cerrado?.Invoke();
        }
        private void AlTerminarNivel()
        {
            bool nivelDesbloqueado = campana.nuevoDesbloqueo();
            Cerrar();
        }

        private void OnDestroy()
        {
            iconoCorreo.onClick.RemoveAllListeners();
            controladorNivel1.NivelTerminado -= AlTerminarNivel;
            controladorNivel1.ReintentoSolicitado -= AlReintentar;
        }

        private void IniciarNivelActual()
        {
            panelMision.Mostrar(campana.NivelActual.Mision);
            controladorNivel1.Iniciar(campana.NivelActual);
        }
        private void AlReintentar()
        {
            campana.ReiniciarNivelActual();
            IniciarNivelActual();
        }
    }
}