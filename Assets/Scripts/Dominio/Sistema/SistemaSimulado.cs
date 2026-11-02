using System.Collections.Generic;

namespace CyberAcademy.Dominio.Sistema
{
    public class SistemaSimulado
    {
        private List <Archivo> archivos;
        private List<Correo> correos;
        public int Reputacion { get; private set; } = 100;

        public SistemaSimulado()
        {
            archivos = new List<Archivo>();
            correos = new List<Correo>();
        }

        public void AgregarArchivo(Archivo archivo)
        {
            archivos.Add(archivo);
        }
        public void AgregarCorreo(Correo correo)
        {
            correos.Add(correo);
        }
        public IReadOnlyList<Archivo> ObtenerArchivos()
        {
            return archivos.AsReadOnly();
        }
        public IReadOnlyList<Correo> ObtenerCorreos()
        {
            return correos.AsReadOnly();
        }
        public int ContarArchivos(EstadoArchivo estado)
        {
            int contador = 0;

            foreach (var item in archivos)
            {
                if (item.Estado == estado)
                {
                    contador++;
                }
            }
            return contador;
        }
        public int CalcularVidaSistema()
        {
            int sanosImportancia = 0;
            int archivosImportancia = 0;

            foreach (var item in archivos)
            {
                if (item.Estado == EstadoArchivo.Sano)
                {
                    sanosImportancia += item.Importancia;
                }
                archivosImportancia += item.Importancia;
            }
            if (archivosImportancia == 0)
            {
                return 100;
            }

            return (int)(100.0 * sanosImportancia / archivosImportancia);
        }

        public void PerderReputacion(int cantidad)
        {
            Reputacion -= cantidad;
            if (Reputacion < 0)
            {
                Reputacion = 0;
            }
        }
    }
}