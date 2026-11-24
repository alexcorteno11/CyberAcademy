
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CyberAcademy.UI
{
    public class ReporteNivel2UI : MonoBehaviour
    {
        [Header("Pantallas")]
        [SerializeField] private GameObject panelCuentas;
        [SerializeField] private GameObject pantallaConfiguracion;
        [SerializeField] private GameObject pantallaResultados;

        [Header("Controlador")]
        [SerializeField] private ControladorNivel2 controladorNivel2;

        private TMP_Text tituloResultado;
        private TMP_Text textoInforme;
        private TMP_Text textoConsejo;
        private Button botonRegresar;

        private readonly Color verde =
            new Color(0.31f, 0.86f, 0.65f);

        private readonly Color rojo =
            new Color(0.96f, 0.42f, 0.42f);

        private void Start()
        {
            if (panelCuentas == null ||
                pantallaConfiguracion == null ||
                pantallaResultados == null ||
                controladorNivel2 == null)
            {
                Debug.LogError(
                    "ReporteNivel2UI: faltan referencias.");
                return;
            }

            CrearInterfaz();

            pantallaResultados.SetActive(false);

            controladorNivel2.SimulacionTerminada +=
                MostrarReporte;
        }

        private TMP_Text CrearTexto(
            string nombre,
            string contenido,
            int tamano,
            float posY,
            float alto)
        {
            GameObject objeto = new GameObject(
                nombre,
                typeof(RectTransform));

            objeto.transform.SetParent(
                pantallaResultados.transform, false);

            RectTransform rect =
                objeto.GetComponent<RectTransform>();

            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(840f, alto);
            rect.anchoredPosition = new Vector2(0f, posY);

            TextMeshProUGUI texto =
                objeto.AddComponent<TextMeshProUGUI>();

            texto.text = contenido;
            texto.fontSize = tamano;
            texto.color = Color.white;
            texto.alignment = TextAlignmentOptions.Center;
            texto.textWrappingMode = TextWrappingModes.Normal;
            texto.enableAutoSizing = true;
            texto.fontSizeMin = 14;
            texto.fontSizeMax = tamano;
            texto.raycastTarget = false;

            return texto;
        }

        private void CrearInterfaz()
        {
            CrearTexto(
                "TituloReporte",
                "REPORTE DE SEGURIDAD",
                32, 325f, 65f);

            tituloResultado = CrearTexto(
                "TituloResultado",
                "RESULTADO",
                30, 225f, 75f);

            textoInforme = CrearTexto(
                "TextoInforme",
                "",
                23, 5f, 350f);

            textoConsejo = CrearTexto(
                "TextoConsejo",
                "",
                20, -225f, 90f);

            GameObject objetoBoton = new GameObject(
                "BotonRegresarCuentas",
                typeof(RectTransform),
                typeof(Image),
                typeof(Button));

            objetoBoton.transform.SetParent(
                pantallaResultados.transform, false);

            RectTransform rect =
                objetoBoton.GetComponent<RectTransform>();

            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(420f, 65f);
            rect.anchoredPosition =
                new Vector2(0f, -345f);

            Image imagen =
                objetoBoton.GetComponent<Image>();

            imagen.color =
                new Color(0.12f, 0.56f, 0.39f);

            botonRegresar =
                objetoBoton.GetComponent<Button>();

            botonRegresar.targetGraphic = imagen;

            botonRegresar.onClick.AddListener(
                VolverACuentas);

            GameObject objetoTexto = new GameObject(
                "TextoBoton",
                typeof(RectTransform));

            objetoTexto.transform.SetParent(
                objetoBoton.transform, false);

            RectTransform rectTexto =
                objetoTexto.GetComponent<RectTransform>();

            rectTexto.anchorMin = Vector2.zero;
            rectTexto.anchorMax = Vector2.one;
            rectTexto.offsetMin = Vector2.zero;
            rectTexto.offsetMax = Vector2.zero;

            TextMeshProUGUI textoBoton =
                objetoTexto.AddComponent<TextMeshProUGUI>();

            textoBoton.text = "VOLVER A LAS CUENTAS";
            textoBoton.fontSize = 23;
            textoBoton.color = Color.white;
            textoBoton.alignment =
                TextAlignmentOptions.Center;
            textoBoton.raycastTarget = false;
        }

        private void MostrarReporte(bool protegida)
        {
            // Ocultar los subtitulos de la pantalla anterior.
            Transform subtitulo = transform.Find("Subtitulo");
            Transform descripcion = transform.Find("Descripcion");

            if (subtitulo != null)
                subtitulo.gameObject.SetActive(false);

            if (descripcion != null)
                descripcion.gameObject.SetActive(false);

            // El controlador ya escribió el informe real.
            Transform objetoEstado =
                pantallaConfiguracion.transform.Find(
                    "TextoEstado");

            if (objetoEstado == null)
            {
                Debug.LogError(
                    "No se encontro TextoEstado.");
                return;
            }

            TMP_Text estado =
                objetoEstado.GetComponent<TMP_Text>();

            if (estado == null)
                return;

            tituloResultado.text = protegida
                ? "CUENTA PROTEGIDA"
                : "CUENTA COMPROMETIDA";

            tituloResultado.color =
                protegida ? verde : rojo;


            textoInforme.text = estado.text
                .Replace("¡SISTEMA PROTEGIDO!\n", "")
                .Replace("¡ALERTA DE SEGURIDAD!\n", "")
                .Replace("CUENTA PROTEGIDA\n", "")
                .Replace("CUENTA COMPROMETIDA\n", "")
                .Replace("Cuenta evaluada. Usa VOLVER para continuar la misión.", "");


            textoConsejo.text = protegida
                ? "El ataque fue detenido. Recuerda que " +
                  "el firewall no reemplaza una contraseña segura."
                : "El ataque superó las defensas. " +
                  "Revisa la contraseña y el límite de intentos.";

            textoConsejo.color =
                protegida ? verde : rojo;

            pantallaConfiguracion.SetActive(false);
            panelCuentas.SetActive(false);
            pantallaResultados.SetActive(true);
        }

        private void VolverACuentas()
        {
            Transform subtitulo = transform.Find("Subtitulo");
            Transform descripcion = transform.Find("Descripcion");

            if (subtitulo != null)
                subtitulo.gameObject.SetActive(true);

            if (descripcion != null)
                descripcion.gameObject.SetActive(true);
            pantallaResultados.SetActive(false);
            pantallaConfiguracion.SetActive(false);
            panelCuentas.SetActive(true);
        }

        private void OnDestroy()
        {
            if (controladorNivel2 != null)
            {
                controladorNivel2.SimulacionTerminada -=
                    MostrarReporte;
            }

            if (botonRegresar != null)
            {
                botonRegresar.onClick.RemoveListener(
                    VolverACuentas);
            }
        }
    }
}

