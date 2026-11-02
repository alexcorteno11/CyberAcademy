using CyberAcademy.Dominio.Ataques;

namespace CyberAcademy.Dominio.Defensas
{
    public interface IDefensa 
    {
        string Nombre { get; }
        int Costo { get; }
        bool PuedeBloquear(TipoAtaque tipoAtaque);

    }
}