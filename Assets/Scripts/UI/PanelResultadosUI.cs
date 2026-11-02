using UnityEngine;
using TMPro;
using CyberAcademy.Dominio.Juego;

namespace CyberAcademy.UI
{
    public class PanelResultadosUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text tituloTexto;
        [SerializeField] private TMP_Text resumenTexto;

        
        public void Mostrar(Nivel nivel)
        {
           
            gameObject.SetActive(true);
            if (nivel.Superado())
            {
                tituloTexto.text = "<color=#2E9E6B>Nivel superado</color>";
                resumenTexto.text = $"{nivel.ResultadoFinal.Mensaje}\nVida restante: {nivel.Sistema.CalcularVidaSistema()}\nReputación restante: {nivel.Sistema.Reputacion}";

            }
            else
            {
                tituloTexto.text = "<color=#C8443C>Nivel no superado</color>";
                resumenTexto.text = $"{nivel.ResultadoFinal.Mensaje}\nVida restante: {nivel.Sistema.CalcularVidaSistema()}\nReputación restante: {nivel.Sistema.Reputacion}";
            }
        }
    }
}