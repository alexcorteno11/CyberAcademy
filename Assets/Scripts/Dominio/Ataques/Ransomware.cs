using System;
using System.Collections.Generic;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Ataques
{
    public class Ransomware : Ataque, IAtaquePorPasos
    {
        private const int ArchivosPorTurno = 2;
        private const int TurnosDelAtaque = 5;

        private readonly List<Archivo> archivosAtacados;

        public int TurnosTotales { get; private set; }
        public int TurnoActual { get; private set; }
        public bool HaTerminado => TurnoActual >= TurnosTotales;

        public Ransomware(int dificultad) : base("Ransomware", TipoAtaque.Ransomware, dificultad)
        {
            TurnosTotales = TurnosDelAtaque;
            TurnoActual = 0;
            archivosAtacados = new List<Archivo>();
        }

        public override ResultadoAtaque Ejecutar(SistemaSimulado objetivo, IEnumerable<IDefensa> defensas)
        {
            if (HaTerminado)
            {
                throw new InvalidOperationException("El ataque de ransomware ya terminó.");
            }

            ResultadoAtaque resultado = null;
            while (!HaTerminado)
            {
                resultado = EjecutarPaso(objetivo, defensas);
            }
            return resultado;
        }

        public ResultadoAtaque EjecutarPaso(SistemaSimulado objetivo, IEnumerable<IDefensa> defensas)
        {
            if (objetivo == null)
            {
                throw new ArgumentNullException(nameof(objetivo));
            }
            if (defensas == null)
            {
                throw new ArgumentNullException(nameof(defensas));
            }
            if (HaTerminado)
            {
                throw new InvalidOperationException("El ataque de ransomware ya terminó.");
            }

            if (FueBloqueado(defensas))
            {
                TurnoActual = TurnosTotales;
                return new ResultadoAtaque(true, 0, "Las defensas detuvieron el ransomware antes de que cifrara un solo archivo.");
            }

            TurnoActual++;

            if (HaTerminado)
            {
                return CrearResumen(objetivo);
            }
            return CifrarSiguientes(objetivo);
        }

        private ResultadoAtaque CifrarSiguientes(SistemaSimulado objetivo)
        {
            var cifradosEsteTurno = new List<string>();

            for (int i = 0; i < ArchivosPorTurno; i++)
            {
                Archivo victima = BuscarSiguienteVictima(objetivo);
                if (victima == null)
                {
                    break;
                }
                victima.Cifrar();
                archivosAtacados.Add(victima);
                cifradosEsteTurno.Add(victima.Nombre);
            }

            string mensaje;
            if (cifradosEsteTurno.Count == 0)
            {
                mensaje = $"Turno {TurnoActual} de {TurnosTotales}: el ransomware no encontró más archivos que cifrar.";
            }
            else
            {
                mensaje = $"Turno {TurnoActual} de {TurnosTotales}: el ransomware cifró {string.Join(" y ", cifradosEsteTurno)}.";
            }
            return new ResultadoAtaque(false, cifradosEsteTurno.Count, mensaje);
        }

        private Archivo BuscarSiguienteVictima(SistemaSimulado objetivo)
        {
            Archivo victima = null;
            foreach (var archivo in objetivo.ObtenerArchivos())
            {
                if (archivo.Estado != EstadoArchivo.Sano || archivosAtacados.Contains(archivo))
                {
                    continue;
                }
                if (victima == null || archivo.Importancia > victima.Importancia)
                {
                    victima = archivo;
                }
            }
            return victima;
        }

        private ResultadoAtaque CrearResumen(SistemaSimulado objetivo)
        {
            var recuperados = new List<string>();
            var sinRestaurar = new List<string>();
            var perdidos = new List<string>();

            foreach (var archivo in archivosAtacados)
            {
                if (archivo.Estado == EstadoArchivo.Sano)
                {
                    recuperados.Add(archivo.Nombre);
                }
                else if (archivo.TieneRespaldo)
                {
                    sinRestaurar.Add(archivo.Nombre);
                }
                else
                {
                    perdidos.Add(archivo.Nombre);
                }
            }

            int danio = sinRestaurar.Count + perdidos.Count;

            string mensaje = $"El ransomware cifró {archivosAtacados.Count} archivos y ahora exige un rescate para devolverlos.";
            if (recuperados.Count > 0)
            {
                mensaje += $"\nRecuperados gracias a su respaldo, sin pagar nada: {string.Join(", ", recuperados)}.";
            }
            if (sinRestaurar.Count > 0)
            {
                mensaje += $"\nTenían respaldo, pero no alcanzaron los puntos para restaurarlos: {string.Join(", ", sinRestaurar)}.";
            }
            if (perdidos.Count > 0)
            {
                mensaje += $"\nPerdidos por no tener respaldo: {string.Join(", ", perdidos)}.";
            }
            mensaje += "\nPagar el rescate no garantiza recuperar nada. La defensa real contra el ransomware es respaldar lo importante antes del ataque.";

            return new ResultadoAtaque(danio == 0, danio, mensaje);
        }
    }
}
