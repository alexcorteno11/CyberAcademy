using CyberAcademy.Dominio.Sistema;
using System;

namespace CyberAcademy.Dominio.Juego
{
    public class AccionesTurno
    {
        public int PuntosPorTurno { get; private set; }
        public int PuntosDisponibles { get; private set; }
        public int CostoRespaldo { get; private set; }
        public int CostoRestauracion { get; private set; }

        public AccionesTurno(int puntosPorTurno, int costoRespaldo, int costoRestauracion)
        {
            if(puntosPorTurno <= 0 || costoRespaldo <= 0 || costoRestauracion <= 0)
            {
                throw new ArgumentException("Los valores de puntos por turno, costo de respaldo y costo de restauración deben ser mayores a cero.");

            }
            else
            {
                PuntosPorTurno = puntosPorTurno;
                PuntosDisponibles = puntosPorTurno;
                CostoRespaldo = costoRespaldo;
                CostoRestauracion = costoRestauracion;
            }
            
        }
        public bool Respaldar(Archivo archivo)
        {
            if(PuntosDisponibles < CostoRespaldo || archivo.Estado != EstadoArchivo.Sano || archivo.TieneRespaldo)
            {
                return false;
            }
            archivo.CrearRespaldo();
            PuntosDisponibles -= CostoRespaldo;
            return true;


        }
        public bool Restaurar(Archivo archivo)
        {
            if (PuntosDisponibles >= CostoRestauracion && archivo.Restaurar())
            {
                PuntosDisponibles -= CostoRestauracion;
                return true;
            }
            return false;
        }
        public void ReiniciarPuntos()
        {
            PuntosDisponibles = PuntosPorTurno;
        }
    }
}
