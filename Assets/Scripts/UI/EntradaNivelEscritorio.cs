using UnityEngine;
using System;
using UnityEngine.UI;

namespace CyberAcademy.UI
{
    [Serializable]
    public class EntradaNivelEscritorio
    {
        [SerializeField] private Button icono;
        [SerializeField] private VentanaUI ventana;
        [SerializeField] private ControladorNivelBase controlador;

        public Button Icono => icono;
        public VentanaUI Ventana => ventana;
        public ControladorNivelBase Controlador => controlador;

    }
}