using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Juego;
using CyberAcademy.Dominio.Sistema;
using UnityEngine;
using UnityEngine.UI;

namespace CyberAcademy.UI
{
    public class  ControladorNivel1: ControladorNivelBase
    {
        [SerializeField] private PanelCorreoUI panelCorreo;
        [SerializeField] private PanelResultadosUI panelResultados;
        [SerializeField] private Button botonSeguro;
        [SerializeField] private Button botonPeligroso;
        [SerializeField] private PanelRevisionUI panelRevision;
        [SerializeField] private Button botonRevision;
        [SerializeField] private Button botonTerminarNivel;
        [SerializeField] private Button botonReintentarNivel;

        private int indiceActual = 0;

        private Nivel nivel;

        public override void Iniciar(Nivel nivelRecibido)
        {
            nivel = nivelRecibido;
            indiceActual = 0;
            botonSeguro.interactable = true;
            botonPeligroso.interactable = true;
            panelResultados.gameObject.SetActive(false);
            panelRevision.gameObject.SetActive(false);
            botonSeguro.onClick.RemoveAllListeners();
            botonPeligroso.onClick.RemoveAllListeners();
            botonRevision.onClick.RemoveAllListeners();
            botonTerminarNivel.onClick.RemoveAllListeners();
            botonReintentarNivel.onClick.RemoveAllListeners();

            botonSeguro.onClick.AddListener(() => ClasificarActual(DecisionCorreo.MarcadoSeguro));
            botonPeligroso.onClick.AddListener(() => ClasificarActual(DecisionCorreo.MarcadoPeligroso));
            botonRevision.onClick.AddListener(() => panelRevision.Mostrar(nivel));
            botonTerminarNivel.onClick.AddListener(() => NotificarNivelTerminado());
            botonReintentarNivel.onClick.AddListener(() => NotificarReintento());
            panelCorreo.gameObject.SetActive(true);
            MostrarSiguiente();
        }

        private void ClasificarActual(DecisionCorreo decision)
        {
            Correo correo = nivel.Sistema.ObtenerCorreos()[indiceActual];
            correo.Clasificar(decision);
            indiceActual++;
            MostrarSiguiente();
        }

        private void MostrarSiguiente()
        {
            if(indiceActual < nivel.Sistema.ObtenerCorreos().Count)
            {
                panelCorreo.MostrarCorreo(nivel.Sistema.ObtenerCorreos()[indiceActual]);
            }
            else
            {
                TerminarNivel();
            }
        }

        private void TerminarNivel()
        {
            botonSeguro.interactable = false;
            botonPeligroso.interactable = false;
            ResultadoAtaque resultado = nivel.EjecutarAtaque();
            panelCorreo.gameObject.SetActive(false);
            panelResultados.Mostrar(nivel);
            bool superado = nivel.Superado();
            botonTerminarNivel.gameObject.SetActive(superado);
            botonReintentarNivel.gameObject.SetActive(!superado);

            Debug.Log(resultado.Mensaje);
            if (nivel.Superado())
            {
                
                Debug.Log("¡Felicidades! Has superado el nivel. Vida: " + nivel.Sistema.CalcularVidaSistema() + ", Reputación: " + nivel.Sistema.Reputacion);
            }
            else
            {
    
                Debug.Log("El nivel no fue superado. Vida: " + nivel.Sistema.CalcularVidaSistema() + ", Reputación: " + nivel.Sistema.Reputacion);
            }
           
        }
    }

}


