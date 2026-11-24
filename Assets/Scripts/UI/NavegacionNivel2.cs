
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CyberAcademy.Dominio.Juego;

namespace CyberAcademy.UI
{
    public class NavegacionNivel2 : MonoBehaviour
    {
        [Header("Pantallas")]
        [SerializeField] private GameObject panelCuentas;
        [SerializeField] private GameObject pantallaConfiguracion;

        [Header("Botones")]
        [SerializeField] private Button botonContinuar;
        [SerializeField] private Button botonVolver;

        [Header("Controlador")]
        [SerializeField] private ControladorNivel2 controladorNivel2;

        private Button botonEmpleado;
        private Button botonSupervisor;
        private Button botonAdministrador;
        private TMP_Text textoCuenta;

        private readonly string[] nombres =
        {
            "EMPLEADO",
            "SUPERVISOR",
            "ADMINISTRADOR"
        };

        private readonly bool[] evaluadas = new bool[3];
        private readonly bool[] protegidas = new bool[3];

        private int cuentaActual = 0;
        private int totalEvaluadas = 0;
        private int totalProtegidas = 0;

        private void Start()
        {
            if (panelCuentas == null ||
                pantallaConfiguracion == null ||
                botonContinuar == null ||
                botonVolver == null ||
                controladorNivel2 == null)
            {
                Debug.LogError(
                    "NavegacionNivel2: faltan referencias.");
                return;
            }

            botonEmpleado = BuscarBoton("BotonEmpleado");
            botonSupervisor = BuscarBoton("BotonSupervisor");
            botonAdministrador = BuscarBoton("BotonAdministrador");

            Transform objetoTexto =
                panelCuentas.transform.Find(
                    "TextoCuentaSeleccionada");

            if (botonEmpleado == null ||
                botonSupervisor == null ||
                botonAdministrador == null ||
                objetoTexto == null)
            {
                Debug.LogError(
                    "No se encontraron los botones o el texto.");
                return;
            }

            textoCuenta = objetoTexto.GetComponent<TMP_Text>();

            if (textoCuenta == null)
            {
                Debug.LogError(
                    "Falta TextMeshPro en TextoCuentaSeleccionada.");
                return;
            }

            controladorNivel2.ModoTresCuentas = true;

            controladorNivel2.SimulacionTerminada +=
                RegistrarResultado;

            botonEmpleado.onClick.AddListener(
                () => SeleccionarCuenta(0));

            botonSupervisor.onClick.AddListener(
                () => SeleccionarCuenta(1));

            botonAdministrador.onClick.AddListener(
                () => SeleccionarCuenta(2));

            botonContinuar.onClick.AddListener(
                MostrarConfiguracion);

            botonVolver.onClick.AddListener(
                MostrarCuentas);

            MostrarCuentas();
        }

        private Button BuscarBoton(string nombre)
        {
            Transform objeto =
                panelCuentas.transform.Find(nombre);

            return objeto != null
                ? objeto.GetComponent<Button>()
                : null;
        }

        private void SeleccionarCuenta(int indice)
        {
            cuentaActual = indice;
            ActualizarMarcador();
        }
        private void MostrarConfiguracion()
        {
            if (evaluadas[cuentaActual])
                return;

            // Empleado = 1
            // Supervisor = 2
            // Administrador = 3
            controladorNivel2.DificultadCuenta = cuentaActual + 1;

            controladorNivel2.Iniciar(
                CreadorDeNivel2.CrearNivel2());

            panelCuentas.SetActive(false);
            pantallaConfiguracion.SetActive(true);
        }

        private void MostrarCuentas()
        {
            pantallaConfiguracion.SetActive(false);
            panelCuentas.SetActive(true);

            if (textoCuenta != null)
                ActualizarMarcador();
        }

        private void RegistrarResultado(bool protegida)
        {
            if (evaluadas[cuentaActual])
                return;

            evaluadas[cuentaActual] = true;
            protegidas[cuentaActual] = protegida;

            totalEvaluadas++;

            if (protegida)
                totalProtegidas++;

            Debug.Log(
                nombres[cuentaActual] +
                ": " +
                (protegida ? "PROTEGIDA" : "COMPROMETIDA"));

            ActualizarMarcador();

            if (totalEvaluadas == 3 && totalProtegidas >= 2)
            {
                controladorNivel2.CompletarMision();
            }

        }

        private void ActualizarMarcador()
        {
            if (textoCuenta == null)
                return;

            if (totalEvaluadas == 3)
            {
                bool victoria = totalProtegidas >= 2;

                textoCuenta.text =
                    (victoria
                        ? "¡MISIÓN SUPERADA!"
                        : "MISIÓN NO SUPERADA") +
                    "\nCuentas protegidas: " +
                    totalProtegidas + "/3";

                textoCuenta.color =
                    victoria ? Color.green : Color.red;

                botonContinuar.interactable = false;
                return;
            }

            string estado;

            if (!evaluadas[cuentaActual])
                estado = "PENDIENTE";
            else if (protegidas[cuentaActual])
                estado = "PROTEGIDA";
            else
                estado = "COMPROMETIDA";

            textoCuenta.text =
                "CUENTA: " + nombres[cuentaActual] +
                " (" + estado + ")\n" +
                "Evaluadas: " + totalEvaluadas +
                "/3 | Protegidas: " + totalProtegidas;

            textoCuenta.color = Color.white;

            botonContinuar.interactable =
                !evaluadas[cuentaActual];
        }

        private void OnDestroy()
        {
            if (controladorNivel2 != null)
                controladorNivel2.SimulacionTerminada -=
                    RegistrarResultado;

            if (botonEmpleado != null)
                botonEmpleado.onClick.RemoveAllListeners();

            if (botonSupervisor != null)
                botonSupervisor.onClick.RemoveAllListeners();

            if (botonAdministrador != null)
                botonAdministrador.onClick.RemoveAllListeners();

            if (botonContinuar != null)
                botonContinuar.onClick.RemoveListener(
                    MostrarConfiguracion);

            if (botonVolver != null)
                botonVolver.onClick.RemoveListener(
                    MostrarCuentas);
        }
    }
}
