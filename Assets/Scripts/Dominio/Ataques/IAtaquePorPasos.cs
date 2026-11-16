
using System.Collections.Generic;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Ataques
{
    public interface IAtaquePorPasos
    {
        int TurnosTotales { get; }
        int TurnoActual { get; }
        bool HaTerminado { get; }

        ResultadoAtaque EjecutarPaso(SistemaSimulado objetivo, IEnumerable<IDefensa> defensas);
    }
}
