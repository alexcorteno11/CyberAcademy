using CyberAcademy.Dominio.Juego;
using UnityEngine;
using System;


namespace CyberAcademy.UI
{
    public class EscritorioUI : MonoBehaviour
    {
        [SerializeField] private GameObject canvasEscritorio;
        [SerializeField] private PanelMisionUI panelMision;
        [SerializeField] private EntradaNivelEscritorio[] entradas;

        public event Action Cerrado;

        private Campana campana;
        private int indiceActual = -1;


        private void Start()
        {
            
            for (int i = 0; i < entradas.Length; i++)
            {
                if (entradas[i].Icono == null || entradas[i].Controlador == null)
                {
                    continue;
                }

                int indice = i;
                entradas[i].Controlador.NivelTerminado += AlTerminarNivel;
                entradas[i].Controlador.ReintentoSolicitado += AlReintentar;
                entradas[i].Icono.onClick.AddListener(() => AlPulsarIcono(indice));
                entradas[i].Icono.interactable = false;
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
        
        private void AlPulsarIcono(int indice)
        {
            entradas[indice].Ventana.Abrir();
        }

        public bool Abrir(Campana campanaRecibida, int indiceNivel)
        {
            if (indiceNivel >= entradas.Length || indiceNivel < 0 || entradas[indiceNivel].Icono == null || entradas[indiceNivel].Controlador == null)
            {
                return false;
            }
            
            this.campana = campanaRecibida;
            if (!campana.SeleccionarNivel(indiceNivel))
            {
                return false;
            }
            indiceActual = indiceNivel;
            canvasEscritorio.SetActive(true);
            for (int i = 0; i < entradas.Length; i++)
            {
                if (entradas[i].Icono == null)
                {
                    continue;
                }
                if (i == indiceActual)
                {
                    entradas[i].Icono.interactable = true;
                    continue;
                }
                entradas[i].Icono.interactable = false;
                
            }
            
            IniciarNivelActual();
            return true;
        }

        public void Cerrar()
        {
            foreach (var entrada in entradas)
            {
                if (entrada == null || entrada.Ventana == null)
                {
                    continue;
                }
                entrada.Ventana.Cerrar();
            }
            indiceActual = -1;
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
            
            foreach (var entrada in entradas)
            {
                if (entrada.Icono == null || entrada.Controlador == null)
                {
                    continue;
                }
                entrada.Icono.onClick.RemoveAllListeners();
                entrada.Controlador.NivelTerminado -= AlTerminarNivel;
                entrada.Controlador.ReintentoSolicitado -= AlReintentar;
            }
            
        }

        private void IniciarNivelActual()
        {
            panelMision.Mostrar(campana.NivelActual.Mision);
            entradas[indiceActual].Controlador.Iniciar(campana.NivelActual);
        }
        private void AlReintentar()
        {
            campana.ReiniciarNivelActual();
            IniciarNivelActual();
        }
    }
}