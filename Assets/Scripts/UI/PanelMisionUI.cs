using TMPro;
using UnityEngine;

namespace CyberAcademy.UI
{

    public class PanelMisionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text textoMision;

        public void Mostrar(string texto)
        {
            textoMision.text = "- " + texto;

        }

    }
}