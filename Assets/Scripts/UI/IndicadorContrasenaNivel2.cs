
using UnityEngine;
using TMPro;
using CyberAcademy.Dominio.Ataques;

namespace CyberAcademy.UI
{
    public class IndicadorContrasenaNivel2 : MonoBehaviour
    {
        [Header("Elementos de la interfaz")]
        [SerializeField] private TMP_InputField campoContrasena;
        [SerializeField] private TMP_Text textoIndicador;

        [Header("Colores")]
        [SerializeField]
        private Color colorDebil =
            new Color(1f, 0.35f, 0.35f);

        [SerializeField]
        private Color colorMejorable =
            new Color(1f, 0.75f, 0.25f);

        [SerializeField]
        private Color colorResistente =
            new Color(0.25f, 0.9f, 0.55f);

        private void Start()
        {
            if (campoContrasena == null || textoIndicador == null)
            {
                Debug.LogWarning(
                    "IndicadorNivel2: faltan referencias.");
                return;
            }

            campoContrasena.onValueChanged.AddListener(
                ActualizarIndicador);

            ActualizarIndicador(campoContrasena.text);
        }

        private void OnDestroy()
        {
            if (campoContrasena != null)
            {
                campoContrasena.onValueChanged.RemoveListener(
                    ActualizarIndicador);
            }
        }

        private void ActualizarIndicador(string contrasena)
        {
            if (textoIndicador == null)
                return;

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                textoIndicador.text =
                    "RESISTENCIA: SIN EVALUAR";
                textoIndicador.color = Color.white;
                return;
            }

            string resultado =
                FuerzaBruta.EvaluarContrasena(contrasena);

            textoIndicador.text = resultado;

            if (resultado.StartsWith("DÉBIL") ||
                resultado.StartsWith("MUY DÉBIL"))
            {
                textoIndicador.color = colorDebil;
            }
            else if (resultado.StartsWith("MEJORABLE"))
            {
                textoIndicador.color = colorMejorable;
            }
            else
            {
                textoIndicador.color = colorResistente;
            }
        }
    }
}
