using UnityEngine;

namespace CyberAcademy.Hub
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class MovimientoJugador : MonoBehaviour
    {
        [SerializeField] private float velocidad = 3f;
        private Rigidbody2D cuerpo;
        private Vector2 direccion;
        private bool puedeMoverse = true;

        public Vector2 Direccion => direccion;

        private void Awake()
        {
            cuerpo = GetComponent<Rigidbody2D>();
        }

      
        private void Update()
        {
            if (puedeMoverse)
            {
                direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            }
            else
            {
                direccion = Vector2.zero;
            }


        }
        private void FixedUpdate()
        {
            cuerpo.linearVelocity = direccion * velocidad;
        }

        public void ActivarMovimiento(bool activo)
        {
            puedeMoverse = activo;
            if(!activo)
            {
                cuerpo.linearVelocity = Vector2.zero;
                direccion = Vector2.zero;
            }
        }
    }
}