
using System;
using System.Collections.Generic;
using CyberAcademy.Dominio.Defensas;
using CyberAcademy.Dominio.Sistema;

namespace CyberAcademy.Dominio.Ataques
{
    public class FuerzaBruta : Ataque
    {
        private string contrasena;
        private int dificultadCuenta = 1;

        private static readonly HashSet<string> comunes =
            new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
            {
                "123456",
                "12345678",
                "123456789",
                "password",
                "password123",
                "qwerty",
                "admin",
                "admin123",
                "abc123",
                "cyberacademy",
                "contraseña123"
            };

        public FuerzaBruta(int dificultad)
            : base(
                "Fuerza Bruta",
                TipoAtaque.FuerzaBruta,
                dificultad)
        {
        }

        // 1 = Empleado
        // 2 = Supervisor
        // 3 = Administrador
        public void ConfigurarEscenario(int dificultad)
        {
            dificultadCuenta =
                Math.Max(1, Math.Min(3, dificultad));
        }

        public void EstablecerContrasena(string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(nuevaContrasena))
            {
                throw new ArgumentException(
                    "La contraseña no puede estar vacía.");
            }

            contrasena = nuevaContrasena;
        }

        private static bool TienePatron(string clave)
        {
            string texto = clave.ToLowerInvariant();

            return texto.Contains("1234") ||
                   texto.Contains("abcd") ||
                   texto.Contains("qwerty") ||
                   texto.Contains("password") ||
                   texto.Contains("admin");
        }

        private static int ContarTipos(string clave)
        {
            bool minusculas = false;
            bool mayusculas = false;
            bool numeros = false;
            bool simbolos = false;

            foreach (char c in clave)
            {
                if (char.IsLower(c))
                    minusculas = true;
                else if (char.IsUpper(c))
                    mayusculas = true;
                else if (char.IsDigit(c))
                    numeros = true;
                else
                    simbolos = true;
            }

            int total = 0;

            if (minusculas) total++;
            if (mayusculas) total++;
            if (numeros) total++;
            if (simbolos) total++;

            return total;
        }

        public static string EvaluarContrasena(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return "Escribe una contraseña ficticia.";

            if (comunes.Contains(clave))
            {
                return "MUY DÉBIL: Es una contraseña " +
                       "común y predecible.";
            }

            if (TienePatron(clave))
            {
                return "DÉBIL: Contiene un patrón " +
                       "muy conocido.";
            }

            if (clave.Length < 8)
            {
                return "DÉBIL: Es demasiado corta. " +
                       "Prueba una contraseña más larga.";
            }

            if (clave.Length < 12 ||
                ContarTipos(clave) < 3)
            {
                return "MEJORABLE: Utiliza mayor longitud " +
                       "y evita patrones predecibles.";
            }

            return "MÁS RESISTENTE: Buena longitud " +
                   "y variedad de caracteres.";
        }

        // Intentos ficticios para el minijuego.
        // No representan tiempos reales de descifrado.
        private int CalcularIntentosDelJuego()
        {
            int intentos;

            if (comunes.Contains(contrasena))
            {
                intentos = 1;
            }
            else if (TienePatron(contrasena))
            {
                intentos = 3;
            }
            else if (contrasena.Length < 8)
            {
                intentos = 6;
            }
            else if (contrasena.Length < 12)
            {
                intentos = 9;
            }
            else if (ContarTipos(contrasena) < 3)
            {
                intentos = 12;
            }
            else
            {
                intentos = 18;
            }

            // Mayor dificultad = escenario más exigente.
            int penalizacion = (dificultadCuenta - 1) * 2;

            return Math.Max(1, intentos - penalizacion);
        }

        public int ConsultarIntentosSimulados()
        {
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new InvalidOperationException(
                    "Primero configura una contraseña ficticia.");
            }

            return CalcularIntentosDelJuego();
        }

        private string NombreCuenta()
        {
            switch (dificultadCuenta)
            {
                case 2:
                    return "SUPERVISOR";

                case 3:
                    return "ADMINISTRADOR";

                default:
                    return "EMPLEADO";
            }
        }

        public override ResultadoAtaque Ejecutar(
            SistemaSimulado objetivo,
            IEnumerable<IDefensa> defensas)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new InvalidOperationException(
                    "Primero configura una contraseña.");
            }

            try
            {
                int intentosNecesarios =
                    CalcularIntentosDelJuego();

                int limiteFirewall = 10;
                bool firewallActivo = false;

                if (defensas != null)
                {
                    foreach (IDefensa defensa in defensas)
                    {
                        if (defensa is ILimitadorDeIntentos limitador)
                        {
                            firewallActivo = true;

                            limiteFirewall = Math.Min(
                                limiteFirewall,
                                limitador.LimiteIntentos);
                        }
                    }
                }

                string diagnostico =
                    EvaluarContrasena(contrasena);

                // Si el límite es menor que los intentos
                // necesarios, el firewall detiene el ataque.
                bool protegida =
                    !firewallActivo ||
                    intentosNecesarios > limiteFirewall;

                string informe =
                    "Cuenta: " + NombreCuenta() + "\n" +
                    "Intentos del ataque simulado: " +
                    intentosNecesarios + "\n" +
                    "Límite del firewall: " +
                    (firewallActivo
                        ? limiteFirewall.ToString()
                        : "No configurado") +
                    "\n\n" +
                    "ANÁLISIS DE CONTRASEÑA:\n" +
                    diagnostico + "\n\n";

                if (protegida)
                {
                    return new ResultadoAtaque(
                        true,
                        0,
                        "CUENTA PROTEGIDA\n" +
                        informe +
                        "El ataque fue detenido en " +
                        "esta simulación.\n" +
                        "El firewall limitó los intentos."
                    );
                }

                objetivo.PerderReputacion(40);

                return new ResultadoAtaque(
                    false,
                    40,
                    "CUENTA COMPROMETIDA\n" +
                    informe +
                    "El ataque simulado alcanzó " +
                    "la contraseña antes del bloqueo.\n" +
                    "La empresa perdió reputación."
                );
            }
            finally
            {
                contrasena = null;
            }
        }
    }
}
