using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Juego;
using CyberAcademy.Dominio.Sistema;
using UnityEngine;
using UnityEngine.UI;

namespace CyberAcademy.UI
{
    public class  ControladorNivel1: MonoBehaviour
    {
        [SerializeField] private PanelCorreoUI panelCorreo;
        [SerializeField] private PanelResultadosUI panelResultados;
        [SerializeField] private Button botonSeguro;
        [SerializeField] private Button botonPeligroso;
        [SerializeField] private PanelRevisionUI panelRevision;
        [SerializeField] private Button botonRevision;

        private int indiceActual = 0;

        private Nivel nivel;

        public void Iniciar(Nivel nivelRecibido)
        {
            nivel = nivelRecibido;
            MostrarSiguiente();
            botonSeguro.onClick.AddListener(() => ClasificarActual(DecisionCorreo.MarcadoSeguro));
            botonPeligroso.onClick.AddListener(() => ClasificarActual(DecisionCorreo.MarcadoPeligroso));
            botonRevision.onClick.AddListener(() => panelRevision.Mostrar(nivel));
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


