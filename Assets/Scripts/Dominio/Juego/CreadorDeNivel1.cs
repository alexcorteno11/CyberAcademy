using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Sistema;


namespace CyberAcademy.Dominio.Juego
{

    public static class CreadorDeNivel1    
    {
        public static Nivel CrearNivel1()
        {
            var sistema = new SistemaSimulado();
            sistema.AgregarArchivo(new Archivo("Balance.xlsx", 5));
            sistema.AgregarArchivo(new Archivo("Fotos.zip", 1));
            sistema.AgregarArchivo(new Archivo("Contrato.pdf", 4));
            var correo = new Correo("carolina.mendez@nexora.com", "Junta de equipo mañana a las 10:00",
                "Hola equipo, mañana tenemos junta a las 10:00 en la sala 2 para revisar el avance del trimestre. Si no puedes asistir, avísame hoy. Saludos, Carolina.", false);
            AgregarCorreo(sistema, correo, "El dominio coincide con el de la empresa y el mensaje no pide datos ni clics.");
            correo = new Correo("entregas@envioexpress-mx.com", "Tu paquete no pudo ser entregado",
                "Intentamos entregar tu paquete pero la dirección estaba incompleta. Actualiza tus datos y paga $29 de reenvío: http://envio-express.co/pago", true);
            AgregarCorreo(sistema, correo, "No esperabas ningún paquete; piden un pago pequeño para robar datos de tarjeta; el enlace es de otro dominio.");
            correo = new Correo("ti@nexora.com", "Mantenimiento del servidor este sábado",
                "Este sábado de 22:00 a 23:00 el servidor de archivos estará fuera de servicio por mantenimiento. No necesitas hacer nada.", false);
            AgregarCorreo(sistema, correo, "Es informativo, viene del dominio correcto y no te pide contraseñas ni clics.");
            correo = new Correo("luis.ortega@nexora.com", "Reporte de ventas de octubre",
                "Ya dejé el reporte de octubre en la carpeta compartida del equipo, como acordamos en la junta. Cualquier duda me dices.", false);
            AgregarCorreo(sistema, correo, "Es una conversación de trabajo normal, con el dominio correcto y sin enlaces.");
            correo = new Correo("premios@sorteos-viajeros.com", "¡Ganaste un viaje a Cancún!",
                "Felicidades, fuiste seleccionado entre miles de participantes. Para reclamar tu premio, paga $350 de gastos de envío en: http://sorteos-viajeros.com/reclamar", true);
            AgregarCorreo(sistema, correo, "Nunca participaste en ese sorteo; piden un pago por adelantado; el remitente es desconocido.");
            correo = new Correo("rh@nexora.com", "Recordatorio: registro de vacaciones",
                "Recuerda registrar tus vacaciones en la intranet antes del viernes. Entra desde el portal de siempre; no necesitas responder este correo.", false);
            AgregarCorreo(sistema, correo, "No incluye enlaces externos y remite a la intranet habitual.");
            correo = new Correo("alertas@banco-aurora.net", "URGENTE: tu cuenta será bloqueada",
                "Estimado cliente: detectamos actividad sospechosa. Verifica tus datos en las próximas 24 horas o tu cuenta será suspendida: http://bancoaurora-verifica.com/acceso", true);
            AgregarCorreo(sistema, correo, "El dominio no es el oficial del banco; el saludo es genérico; presiona con urgencia y amenaza; el enlace lleva a otro dominio.");

            correo = new Correo("carolina.mendez@nexora-mx.com", "Necesito un favor urgente",
                "Estoy en una reunión y no puedo hablar. Necesito que compres 5 tarjetas de regalo de $1,000 para un cliente y me mandes los códigos por aquí. Te lo reembolso hoy. No le digas a nadie.", true);
            AgregarCorreo(sistema, correo, "El dominio imita al de la empresa pero es distinto (nexora-mx.com); pide dinero o códigos con urgencia y secreto; un jefe real no lo haría por correo.");



            return new Nivel("Nivel 1", sistema, new Phishing(1), 50, 71, "Revisa los correos del sistema");
        }
        
        private static void AgregarCorreo(SistemaSimulado sistema, Correo correo, params string[] pistas)
        {
            foreach (var pista in pistas)
            {
                correo.AgregarPista(pista);
            }
            sistema.AgregarCorreo(correo);
        }

    }
}