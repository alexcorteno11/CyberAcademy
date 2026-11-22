using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Juego
{
    public static class CreadorDeNivel3
    {
        public static Nivel CrearNivel3()
        {
            SistemaSimulado sistema = new SistemaSimulado();

            sistema.AgregarArchivo(new Archivo("Inventario.xlsx", 4));
            sistema.AgregarArchivo(new Archivo("Contratos.pdf", 4));
            sistema.AgregarArchivo(new Archivo("Notas.txt", 2));

            Malware ataque = new Malware(1);

            Descarga descarga1 = new Descarga(
                "LectorPDF.exe",
                "Portal interno de TI",
                "Entraste desde el marcador de la empresa y encontraste el programa que solicitaste.",
                false);

            Descarga descarga2 = new Descarga(
                "Factura.pdf.exe",
                "Correo de un remitente desconocido",
                "Habla de una compra que no realizaste. El archivo termina en .exe.",
                true);

            Descarga descarga3 = new Descarga(
                "EditorGratis.zip",
                "Anuncio de una página de descargas",
                "Promete desbloquear un programa de paga y pide desactivar el antivirus.",
                true);

            Descarga descarga4 = new Descarga(
                "PlantillaInventario.xlsx",
                "Carpeta compartida del equipo",
                "Tu compañero dejó la plantilla en la carpeta que acordaron durante la junta.",
                false);

            ataque.AgregarDescarga(descarga1);
            ataque.AgregarDescarga(descarga2);
            ataque.AgregarDescarga(descarga3);
            ataque.AgregarDescarga(descarga4);

            Antivirus antivirus = new Antivirus(20);

            Nivel nivel = new Nivel(
                "Nivel 3 - Malware",
                sistema,
                ataque,
                100,
                0,
                "Abre el Navegador. Revisa las fuentes y las pistas de las descargas. " +
                "Puedes escanearlas antes de aceptarlas o rechazarlas. " +
                "Termina la revisión conservando todos los archivos sanos.");

            nivel.AgregarDefensa(antivirus);

            return nivel;
        }
    }
}