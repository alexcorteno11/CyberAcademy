namespace CyberAcademy.Dominio.Ataques
{
    public class ResultadoAtaque
    {
        public bool Bloqueado { get; private set; }
        public int Danio { get; private set; }
        public string Mensaje { get; private set; }
        public ResultadoAtaque(bool bloqueado, int danio, string mensaje)
        {
            Bloqueado = bloqueado;
            Danio = danio;
            Mensaje = mensaje;
            
        }
    }
}