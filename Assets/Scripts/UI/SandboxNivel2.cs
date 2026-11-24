
using System;
using System.Collections.Generic;
using CyberAcademy.Dominio.Juego;
using UnityEngine;

namespace CyberAcademy.UI
{
    public class SandboxNivel2 : MonoBehaviour
    {
        [SerializeField]
        private ControladorNivelBase controlador;

        private Campana campana;

        private void Start()
        {
            if (controlador == null)
            {
                Debug.LogError(
                    "Falta asignar el controlador del Nivel 2.");
                return;
            }

            campana = new Campana(
                new List<Func<Nivel>>
                {
                    CreadorDeNivel2.CrearNivel2
                });

            controlador.NivelTerminado += AlTerminar;
            controlador.ReintentoSolicitado += AlReintentar;

            controlador.Iniciar(campana.NivelActual);
        }

        private void AlTerminar()
        {
            Debug.Log("¡Nivel 2 superado!");

            // Reiniciar para permitir volver a jugar.
            AlReintentar();
        }

        private void AlReintentar()
        {
            campana.ReiniciarNivelActual();
            controlador.Iniciar(campana.NivelActual);
        }

        private void OnDestroy()
        {
            if (controlador != null)
            {
                controlador.NivelTerminado -= AlTerminar;
                controlador.ReintentoSolicitado -= AlReintentar;
            }
        }
    }
}
