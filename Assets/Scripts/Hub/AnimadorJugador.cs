using UnityEngine;

namespace CyberAcademy.Hub
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MovimientoJugador))]
    public class AnimadorJugador : MonoBehaviour
    {
        private static readonly int ParametroDirX = Animator.StringToHash("DirX");
        private static readonly int ParametroDirY = Animator.StringToHash("DirY");
        private static readonly int ParametroMoviendo = Animator.StringToHash("Moviendo");

        private Animator animador;
        private MovimientoJugador movimiento;
        private Vector2 ultimaDireccion = Vector2.down;

        private void Awake()
        {
            animador = GetComponent<Animator>();
            movimiento = GetComponent<MovimientoJugador>();

        }

        private void Update()
        {
            Vector2 direccion = movimiento.Direccion;
            if (direccion.sqrMagnitude > 0)
            {
                ultimaDireccion = ObtenerDireccionCardinal(direccion);
                animador.SetFloat(ParametroDirX, ultimaDireccion.x);
                animador.SetFloat(ParametroDirY, ultimaDireccion.y);
                animador.SetBool(ParametroMoviendo, true);
            }
            else
            {
                animador.SetBool(ParametroMoviendo, false);
            }

        }

        private Vector2 ObtenerDireccionCardinal(Vector2 direccion)
        {
            if (Mathf.Abs(direccion.x) >= Mathf.Abs(direccion.y))
            {
                return new Vector2(Mathf.Sign(direccion.x), 0);
            }
            else
            {
                return new Vector2(0, Mathf.Sign(direccion.y));
            }
        }
        
    }
}