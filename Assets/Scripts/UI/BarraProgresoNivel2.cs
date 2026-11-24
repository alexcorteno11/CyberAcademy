
using UnityEngine;
using UnityEngine.UI;

namespace CyberAcademy.UI
{
    public class BarraProgresoNivel2 : MonoBehaviour
    {
        [SerializeField] private Image relleno;

        public void ActualizarProgreso(float progreso)
        {
            if (relleno == null)
                return;

            relleno.fillAmount = Mathf.Clamp01(progreso);
        }

        public void Reiniciar()
        {
            ActualizarProgreso(0f);
        }
    }
}
