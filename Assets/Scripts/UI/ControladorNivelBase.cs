using UnityEngine;
using System;
using CyberAcademy.Dominio.Juego;

namespace CyberAcademy.UI
{
    public abstract class ControladorNivelBase : MonoBehaviour
    {
        public event Action NivelTerminado;

        public abstract void Iniciar(Nivel nivelRecibido);

        protected void NotificarNivelTerminado()
        {
            NivelTerminado?.Invoke();
        }
    }

}

