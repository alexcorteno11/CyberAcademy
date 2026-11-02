
using UnityEngine;
using UnityEngine.UI;

namespace CyberAcademy.UI
{


    public class VentanaUI : MonoBehaviour
    {
        [SerializeField] private Button botonCerrar;
        private void Awake()
        {
            botonCerrar.onClick.AddListener(Cerrar);
        }


        public void Abrir() => gameObject.SetActive(true);

        public void Cerrar() => gameObject.SetActive(false);
    }
}