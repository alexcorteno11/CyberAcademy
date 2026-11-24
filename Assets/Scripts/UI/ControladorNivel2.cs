
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CyberAcademy.Dominio.Juego;
using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Defensas;

namespace CyberAcademy.UI
{
    public class ControladorNivel2 : ControladorNivelBase
    {
        [Header("Configuracion")]
        [SerializeField] private TMP_InputField campoContrasena;
        [SerializeField] private Button boton3Intentos;
        [SerializeField] private Button boton5Intentos;
        [SerializeField] private Button boton10Intentos;
        [SerializeField] private Button botonIniciar;

        [Header("Resultados")]
        [SerializeField] private TMP_Text textoEstado;
        [SerializeField] private BarraProgresoNivel2 barraProgreso;
        [SerializeField] private Button botonTerminarNivel;
        [SerializeField] private Button botonReintentarNivel;

        public event Action<bool> SimulacionTerminada;

        public bool ModoTresCuentas { get; set; }

        public bool MisionSuperada { get; private set; } = false;

        public int DificultadCuenta { get; set; } = 1;

        private Nivel nivel;
        private int limiteSeleccionado = 5;
        private bool simulacionEnCurso = false;
        private bool resultadoRegistrado = false;

        public override void Iniciar(Nivel nivelRecibido)
        {
            StopAllCoroutines();

            if (nivelRecibido == null)
            {
                Debug.LogError("No se recibio el Nivel 2.");
                return;
            }

            nivel = nivelRecibido;
            MisionSuperada = false;
            limiteSeleccionado = 5;
            simulacionEnCurso = false;
            resultadoRegistrado = false;

            if (barraProgreso != null)
                barraProgreso.Reiniciar();

            campoContrasena.text = "";
            campoContrasena.interactable = true;

            botonIniciar.interactable = true;
            boton3Intentos.interactable = true;
            boton5Intentos.interactable = true;
            boton10Intentos.interactable = true;

            textoEstado.text =
                "Sistema preparado. Configura tu seguridad.";

            botonTerminarNivel.gameObject.SetActive(false);
            botonReintentarNivel.gameObject.SetActive(false);

            boton3Intentos.onClick.RemoveAllListeners();
            boton5Intentos.onClick.RemoveAllListeners();
            boton10Intentos.onClick.RemoveAllListeners();
            botonIniciar.onClick.RemoveAllListeners();
            botonTerminarNivel.onClick.RemoveAllListeners();
            botonReintentarNivel.onClick.RemoveAllListeners();

            boton3Intentos.onClick.AddListener(
                () => SeleccionarIntentos(3));

            boton5Intentos.onClick.AddListener(
                () => SeleccionarIntentos(5));

            boton10Intentos.onClick.AddListener(
                () => SeleccionarIntentos(10));

            botonIniciar.onClick.AddListener(IniciarSimulacion);

            botonTerminarNivel.onClick.AddListener(
                () => NotificarNivelTerminado());

            botonReintentarNivel.onClick.AddListener(
                () => NotificarReintento());
        }

        private void SeleccionarIntentos(int cantidad)
        {
            if (simulacionEnCurso ||
                resultadoRegistrado ||
                nivel == null)
            {
                return;
            }

            limiteSeleccionado = cantidad;

            textoEstado.text =
                "Firewall configurado: " +
                cantidad + " intentos permitidos.";
        }

        private void IniciarSimulacion()
        {
            if (simulacionEnCurso ||
                resultadoRegistrado ||
                nivel == null)
            {
                return;
            }

            string contrasenaTemporal = campoContrasena.text;

            if (string.IsNullOrWhiteSpace(contrasenaTemporal))
            {
                textoEstado.text =
                    "Primero escribe una contraseña ficticia.";
                return;
            }

            FuerzaBruta ataque =
                nivel.AtaqueNivel as FuerzaBruta;

            if (ataque == null)
            {
                Debug.LogError("El ataque no es FuerzaBruta.");
                return;
            }

            try
            {
                // Configurar la dificultad de la cuenta.
                ataque.ConfigurarEscenario(DificultadCuenta);

                // Establecer contraseña de prueba.
                ataque.EstablecerContrasena(
                    contrasenaTemporal);

                // Consultar intentos antes de ejecutar.
                int intentosNecesarios =
                    ataque.ConsultarIntentosSimulados();

                // Preparar firewall.
                Firewall firewall =
                    new Firewall(limiteSeleccionado);

                if (!nivel.AgregarDefensa(firewall))
                {
                    textoEstado.text =
                        "Este ataque ya fue ejecutado.";
                    return;
                }

                campoContrasena.text = "";
                contrasenaTemporal = null;

                simulacionEnCurso = true;

                campoContrasena.interactable = false;
                botonIniciar.interactable = false;
                boton3Intentos.interactable = false;
                boton5Intentos.interactable = false;
                boton10Intentos.interactable = false;

                // Comenzar monitor animado.
                StartCoroutine(
                    AnimarIntentos(intentosNecesarios));
            }
            catch (Exception excepcion)
            {
                simulacionEnCurso = false;

                Debug.LogError(
                    "Error al preparar simulacion: " +
                    excepcion.GetType().Name);

                textoEstado.text =
                    "No se pudo preparar la simulación.";
            }
        }

        private IEnumerator AnimarIntentos(
            int intentosNecesarios)
        {
            int limite = limiteSeleccionado;

            // Número de intentos que veremos en pantalla.
            int intentosVisibles =
                Mathf.Min(intentosNecesarios, limite);

            if (barraProgreso != null)
                barraProgreso.Reiniciar();

            textoEstado.text =
                "MONITOR DE SEGURIDAD\n" +
                "Preparando ataque simulado...";

            yield return new WaitForSecondsRealtime(0.6f);

            for (int intento = 1;
                 intento <= intentosVisibles;
                 intento++)
            {
                float progreso =
                    (float)intento / limite;

                if (barraProgreso != null)
                    barraProgreso.ActualizarProgreso(
                        progreso);

                textoEstado.text =
                    "MONITOR DE SEGURIDAD\n" +
                    "Intento " + intento +
                    " de " + limite + "\n" +
                    "Comprobando acceso...";

                yield return new WaitForSecondsRealtime(0.45f);
            }

            bool accesoConseguido =
                intentosNecesarios <= limite;

            if (accesoConseguido)
            {
                textoEstado.text =
                    "¡ACCESO COMPROMETIDO!\n" +
                    "El ataque necesitó " +
                    intentosNecesarios +
                    " intentos simulados.\n" +
                    "Firewall: " + limite;
            }
            else
            {
                textoEstado.text =
                    "¡FIREWALL ACTIVADO!\n" +
                    "Límite alcanzado: " +
                    limite + " intentos.\n" +
                    "El ataque fue detenido.";
            }

            yield return new WaitForSecondsRealtime(1.1f);

            // Calcular y registrar el resultado real
            // de la simulación de nuestro videojuego.
            MostrarResultado();
        }

        private void MostrarResultado()
        {
            try
            {
                ResultadoAtaque resultado =
                    nivel.EjecutarAtaque();

                simulacionEnCurso = false;
                resultadoRegistrado = true;

                bool protegida = nivel.Superado();

                textoEstado.text =
                    (protegida
                        ? "¡SISTEMA PROTEGIDO!\n"
                        : "¡ALERTA DE SEGURIDAD!\n") +
                    resultado.Mensaje +
                    "\nReputación: " +
                    nivel.Sistema.Reputacion;

                if (ModoTresCuentas)
                {
                    botonTerminarNivel.gameObject.SetActive(false);
                    botonReintentarNivel.gameObject.SetActive(false);

                    textoEstado.text +=
                        "\n\nCuenta evaluada. Usa VOLVER " +
                        "para continuar la misión.";

                    // Avisar al marcador de las tres cuentas
                    // y a la pantalla de reporte.
                    SimulacionTerminada?.Invoke(protegida);
                }
                else
                {
                    botonTerminarNivel.gameObject.SetActive(
                        protegida);

                    botonReintentarNivel.gameObject.SetActive(
                        !protegida);
                }
            }
            catch (Exception excepcion)
            {
                simulacionEnCurso = false;

                Debug.LogError(
                    "Error durante simulacion: " +
                    excepcion.GetType().Name);

                textoEstado.text =
                    "Ocurrió un problema al ejecutar el ataque.";

                if (!ModoTresCuentas)
                    botonReintentarNivel.gameObject.SetActive(true);
            }
        }
        public void CompletarMision()
        {
            if (!ModoTresCuentas || MisionSuperada)
            {
                return;
            }

            MisionSuperada = true;
            NotificarNivelTerminado();
        }


        private void OnDisable()
        {
            StopAllCoroutines();
            simulacionEnCurso = false;
        }
    }
}
