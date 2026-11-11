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
        [SerializeField] private ControladorNivelBase[] controladoresNivel;
        private ControladorNivelBase controladorActual;


        public event Action Cerrado;

        private Campana campana;

        
       private void Start()
        {
            iconoCorreo.onClick.AddListener(() => ventanaCorreo.Abrir());
            foreach(var controlador in controladoresNivel)
            {
                if(controlador == null)
                {
                    continue;
                }
                controlador.NivelTerminado += AlTerminarNivel;
                controlador.ReintentoSolicitado += AlReintentar;

            }
            
            canvasEscritorio.SetActive(false);
        }
        public void Update()
        {
            if (canvasEscritorio.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                Cerrar();
            }
        }

        public bool Abrir(Campana campanaRecibida, int indiceNivel)
        {
            if(indiceNivel >= controladoresNivel.Length || controladoresNivel[indiceNivel] == null)
            {
                return false;
            }
            
            this.campana = campanaRecibida;
            if (!campana.SeleccionarNivel(indiceNivel))
            {
                return false;
            }
            controladorActual = controladoresNivel[indiceNivel];
            canvasEscritorio.SetActive(true);
            IniciarNivelActual();
            return true;
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
            foreach (var controlador in controladoresNivel)
            {
                if (controlador == null)
                {
                    continue;
                }
                controlador.NivelTerminado -= AlTerminarNivel;
                controlador.ReintentoSolicitado -= AlReintentar;
            }
            
        }

        private void IniciarNivelActual()
        {
            panelMision.Mostrar(campana.NivelActual.Mision);
            controladorActual.Iniciar(campana.NivelActual);
        }
        private void AlReintentar()
        {
            campana.ReiniciarNivelActual();
            IniciarNivelActual();
        }
    }
}