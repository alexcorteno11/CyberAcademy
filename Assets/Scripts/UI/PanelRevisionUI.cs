using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using CyberAcademy.Dominio.Sistema;
using CyberAcademy.Dominio.Juego;


namespace CyberAcademy.UI
{

    public class PanelRevisionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text asuntoTexto;
        [SerializeField] private TMP_Text decisionTexto;
        [SerializeField] private TMP_Text pistasTexto;
        [SerializeField] private TMP_Text contadorTexto;
        [SerializeField] private Button botonAnterior;
        [SerializeField] private Button botonSiguiente;
        [SerializeField] private Button botonVolver;

        private IReadOnlyList<Correo> correos;
        private int indice;

        private void Awake()
        {
            botonAnterior.onClick.AddListener(() =>
            {
                if (indice > 0)
                {
                    indice--;
                    MostrarActual();
                }
            });
            botonSiguiente.onClick.AddListener(() =>
            {
                if (indice < correos.Count - 1)
                {
                    indice++;
                    MostrarActual();
                }
            });
            botonVolver.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }
        public void Mostrar(Nivel nivel)
        {
            correos = nivel.Sistema.ObtenerCorreos();
            if (correos.Count == 0)
            {
                Debug.LogWarning("PanelRevisionUI: el nivel no tiene correos que revisar.");
                return;
            }
            gameObject.SetActive(true);
            
            indice = 0;
            MostrarActual();
        }

        private string NombreDecision(DecisionCorreo decision)
        {
            switch (decision)
            {
                case DecisionCorreo.MarcadoSeguro:
                    return "Seguro";
                case DecisionCorreo.MarcadoPeligroso:
                    return "Peligroso";
                case DecisionCorreo.SinDecidir:
                    return "Sin Decidir";
            }
            return "";
        }
        private void MostrarActual()
        {
            var correo = correos[indice];
            asuntoTexto.text = correo.Asunto;
            string esFraude;
            if (correo.EsFraudulento)
            {
                esFraude = "fraude";
            }
            else
            {
                esFraude = "legitimo";
            }
            if (correo.EstaClasificadoCorrectamente())
            {
                decisionTexto.text = $"Tu decisión: ({NombreDecision(correo.Decision)})\nResultado: <color=#2E9E6B>Correcto</color>\n" +
                    $"Este correo era {esFraude}";
            }
            else
            {
                decisionTexto.text = $"Tu decisión: ({NombreDecision(correo.Decision)})\nResultado: <color=#C8443C>Incorrecto</color>\n" +
                    $"Este correo era {esFraude}";
            }
            
            pistasTexto.text = "Pistas:\n- " + string.Join("\n- ", correo.ObtenerPistas());
            contadorTexto.text = $"Correo {indice + 1} de {correos.Count}";
            

        }

    }
}