
using CyberAcademy.Dominio.Juego;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CyberAcademy.UI
{
    public class EscritorioUI : MonoBehaviour
    {
        [SerializeField] private Button iconoCorreo;
        [SerializeField] private VentanaUI ventanaCorreo;
        [SerializeField] private PanelMisionUI panelMision;
        [SerializeField] private ControladorNivel1 controladorNivel1;
       private void Start()
        {
            var nivel = CreadorDeNiveles.CrearNivel1();
            panelMision.Mostrar(nivel.Mision);
            controladorNivel1.Iniciar(nivel);
            iconoCorreo.onClick.AddListener(() => ventanaCorreo.Abrir());
        }

    }
}