
using CyberAcademy.Dominio.Ataques;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Juego
{
    public static class  CreadorDeCampana
    {
        public static Campana CrearCampanaBase()
        {
            var niveles = new List<Func<Nivel>>();
            niveles.Add(CreadorDeNivel1.CrearNivel1);
            niveles.Add(() => CrearNivelPlaceholder("Nivel 2"));
            niveles.Add(() => CrearNivelPlaceholder("Nivel 3"));
            niveles.Add(() => CrearNivelPlaceholder("Nivel 4"));
            niveles.Add(() => CrearNivelPlaceholder("Nivel 5"));
            return new Campana(niveles);
        }

        private static Nivel CrearNivelPlaceholder(string nombre)
        {
            var sistema = new SistemaSimulado();
            var ataque = new Phishing(1);
            return new Nivel(nombre, sistema, ataque, 50, 50, "Nivel en construccion");
        }

    }
}