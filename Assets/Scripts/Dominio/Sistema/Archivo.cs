namespace CyberAcademy.Dominio.Sistema
{
    public enum EstadoArchivo
    {
        Sano, Infectado, Cifrado
    }
    public class Archivo
    {
        public string Nombre { get; private set; }
        public int Importancia { get; private set; }
        public EstadoArchivo Estado { get; private set; }
        public bool TieneRespaldo { get; private set; }

        public Archivo(string nombre, int importancia)
        {
            Nombre = nombre;
            Importancia = importancia;
            Estado = EstadoArchivo.Sano;
            TieneRespaldo = false;
        }

        public void Infectar()
        {
            if (Estado == EstadoArchivo.Sano)
            {
                Estado = EstadoArchivo.Infectado;
            }
        }

        public void Cifrar()
        {
            Estado = EstadoArchivo.Cifrado;
        }

        public void CrearRespaldo()
        {
            if(Estado == EstadoArchivo.Sano)
            {
                TieneRespaldo = true;
            }
        }

        public bool Restaurar()
        {
            
            if (TieneRespaldo && Estado != EstadoArchivo.Sano)
            {
                Estado = EstadoArchivo.Sano;
                return true;
            }
            return false;
        }
    }
}