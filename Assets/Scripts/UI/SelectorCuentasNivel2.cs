
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CyberAcademy.UI
{
    public class SelectorCuentasNivel2 : MonoBehaviour
    {
        [Header("Botones de cuentas")]
        [SerializeField] private Button botonEmpleado;
        [SerializeField] private Button botonSupervisor;
        [SerializeField] private Button botonAdministrador;

        [Header("Texto informativo")]
        [SerializeField] private TMP_Text textoCuentaSeleccionada;

        private int cuentaActual = 0;

        private void Start()
        {
            if (botonEmpleado != null)
                botonEmpleado.onClick.AddListener(
                    SeleccionarEmpleado);

            if (botonSupervisor != null)
                botonSupervisor.onClick.AddListener(
                    SeleccionarSupervisor);

            if (botonAdministrador != null)
                botonAdministrador.onClick.AddListener(
                    SeleccionarAdministrador);

            MostrarCuenta();
        }

        private void OnDestroy()
        {
            if (botonEmpleado != null)
                botonEmpleado.onClick.RemoveListener(
                    SeleccionarEmpleado);

            if (botonSupervisor != null)
                botonSupervisor.onClick.RemoveListener(
                    SeleccionarSupervisor);

            if (botonAdministrador != null)
                botonAdministrador.onClick.RemoveListener(
                    SeleccionarAdministrador);
        }

        private void SeleccionarEmpleado()
        {
            cuentaActual = 0;
            MostrarCuenta();
        }

        private void SeleccionarSupervisor()
        {
            cuentaActual = 1;
            MostrarCuenta();
        }

        private void SeleccionarAdministrador()
        {
            cuentaActual = 2;
            MostrarCuenta();
        }

        private void MostrarCuenta()
        {
            if (textoCuentaSeleccionada == null)
                return;

            switch (cuentaActual)
            {
                case 0:
                    textoCuentaSeleccionada.text =
                        "CUENTA: EMPLEADO\n" +
                        "Dificultad: FACIL";
                    break;

                case 1:
                    textoCuentaSeleccionada.text =
                        "CUENTA: SUPERVISOR\n" +
                        "Dificultad: MEDIA";
                    break;

                case 2:
                    textoCuentaSeleccionada.text =
                        "CUENTA: ADMINISTRADOR\n" +
                        "Dificultad: DIFICIL";
                    break;
            }
        }
    }
}
