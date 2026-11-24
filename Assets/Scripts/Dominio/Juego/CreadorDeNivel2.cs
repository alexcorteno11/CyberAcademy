
using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Juego
{
    public static class CreadorDeNivel2
    {
        public static Nivel CrearNivel2()
        {
            // Crear el sistema que protegerá el jugador
            var sistema = new SistemaSimulado();

            // Archivos de ejemplo del sistema
            sistema.AgregarArchivo(
                new Archivo("Balance.xlsx", 5));

            sistema.AgregarArchivo(
                new Archivo("Contrato.pdf", 4));

            sistema.AgregarArchivo(
                new Archivo("Fotos.zip", 1));

            // Crear el ataque de fuerza bruta
            var ataque = new FuerzaBruta(1);

            // Misión que verá el jugador
            string mision =
                "Abre Configuración, crea una contraseña ficticia " +
                "y establece el límite de intentos fallidos " +
                "para proteger la cuenta del sistema.";

            // Construir el nivel
            return new Nivel(
                "Nivel 2: Fuerza Bruta",
                sistema,
                ataque,
                50,
                71,
                mision);
        }
    }
}

