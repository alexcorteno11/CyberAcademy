using TMPro;
using UnityEngine;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.UI
{
    public class  PanelCorreoUI: MonoBehaviour
    {
        [SerializeField] private TMP_Text remitenteTexto;
        [SerializeField] private TMP_Text asuntoTexto;
        [SerializeField] private TMP_Text cuerpoTexto;

        public void MostrarCorreo(Correo correo)
        {
            remitenteTexto.text = correo.Remitente;
            asuntoTexto.text = correo.Asunto;
            cuerpoTexto.text = correo.Cuerpo;
        }

    }
}