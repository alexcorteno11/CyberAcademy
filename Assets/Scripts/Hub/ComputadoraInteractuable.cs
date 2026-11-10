using UnityEngine;
using System;

namespace CyberAcademy.Hub
{
    public enum EstadoComputadora
    {
        Bloqueada,
        Disponible,
        Completada
    }
    public class ComputadoraInteractuable : MonoBehaviour
    {
        [SerializeField] private int indiceNivel;
        [SerializeField] private GameObject avisoInteraccion;
        [SerializeField] private SpriteRenderer spriteComputadora;
        [SerializeField] private GameObject marcadorSiguiente;
        
        [SerializeField] private Color colorDisponible = Color.white;
        [SerializeField] private Color colorCompletada = new Color(0.5f, 1f, 0.5f, 1f);
        [SerializeField] private Color colorBloqueada = new Color(0.6f, 0.6f, 0.65f, 1f);
        public event Action<int> InteraccionSolicitada;
        private EstadoComputadora estado = EstadoComputadora.Bloqueada;
        private bool jugadorCerca;

        public int IndiceNivel => indiceNivel;

        private void Awake()
        {
            
            
            ActualizarAviso();
        }
        private void Update()
        {
            if(jugadorCerca && estado != EstadoComputadora.Bloqueada && Input.GetKeyDown(KeyCode.E))
            {
                InteraccionSolicitada?.Invoke(indiceNivel);
                
            }
        }

        private void OnTriggerEnter2D(Collider2D otro)
        {
            if(otro.CompareTag("Player")) 
            {
                jugadorCerca = true;
                ActualizarAviso();
            }
        }

        private void OnTriggerExit2D(Collider2D otro)
        {
            if(otro.CompareTag("Player"))
            {
                jugadorCerca = false;
                ActualizarAviso();
            }
        }
        public void ActualizarEstado(EstadoComputadora nuevoEstado)
        {
            estado = nuevoEstado;
            switch(nuevoEstado)
            {
                case EstadoComputadora.Bloqueada:
                    spriteComputadora.color = colorBloqueada;
                    marcadorSiguiente.SetActive(false);
                    break;
                case EstadoComputadora.Disponible:
                    spriteComputadora.color = colorDisponible;
                    marcadorSiguiente.SetActive(true);
                    break;
                case EstadoComputadora.Completada:
                    spriteComputadora.color = colorCompletada;
                    marcadorSiguiente.SetActive(false);
                    break;
            }
            ActualizarAviso();

        }
        private void ActualizarAviso()
        {
                avisoInteraccion.SetActive(jugadorCerca && estado != EstadoComputadora.Bloqueada);
        }
    }
}