using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Juego
{
    public static class CreadorDeNivel4
    {
        private const int VidaMinimaParaGanar = 70;
        private const int ReputacionMinimaParaGanar = 0;

        private const int PuntosPorTurno = 4;
        private const int CostoRespaldo = 1;
        private const int CostoRestauracion = 2;

        public static Nivel CrearNivel4()
        {
            var sistema = new SistemaSimulado();
            sistema.AgregarArchivo(new Archivo("Fotos_posada.zip", 1));
            sistema.AgregarArchivo(new Archivo("Balance.xlsx", 5));
            sistema.AgregarArchivo(new Archivo("Contratos.pdf", 4));
            sistema.AgregarArchivo(new Archivo("Musica_oficina.mp3", 1));
            sistema.AgregarArchivo(new Archivo("Nomina.xlsx", 5));
            sistema.AgregarArchivo(new Archivo("Facturas.zip", 3));
            sistema.AgregarArchivo(new Archivo("BaseClientes.db", 4));
            sistema.AgregarArchivo(new Archivo("Presentacion.pptx", 2));

            var acciones = new AccionesTurno(PuntosPorTurno, CostoRespaldo, CostoRestauracion);

            string mision = "Abre Archivos. Un ransomware está a punto de cifrar los documentos de la empresa. "
                + "Antes de iniciar el ataque, usa tus puntos para respaldar los archivos más importantes; "
                + "no te alcanzan para todos. Durante el ataque, restaura los archivos cifrados que tengan respaldo. "
                + $"Conserva al menos {VidaMinimaParaGanar}% de la vida del sistema.";

            return new Nivel("Nivel 4", sistema, new Ransomware(1), VidaMinimaParaGanar, ReputacionMinimaParaGanar, mision, acciones);
        }
    }
}
