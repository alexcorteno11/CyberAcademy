using System;
using System.Collections.Generic;

namespace CyberAcademy.Dominio.Juego
{
    public class Campana
    {
        private readonly IReadOnlyList<Func<Nivel>> Niveles;
        private int IndiceNivelActual;
        public Nivel NivelActual { get; private set; }
        public bool EstaCompletada { get; private set; }
        public int TotalNiveles => Niveles.Count;
        public int NivelMaximoDesbloqueado { get; private set; }

        public Campana(IReadOnlyList<Func<Nivel>> niveles)
        {
            if (niveles == null)
            {
                throw new ArgumentNullException(nameof(niveles), "La lista de niveles no puede ser nula.");
            }
            if (niveles.Count == 0)
            {
                throw new ArgumentException("La lista de niveles no puede estar vacía.");
                
            }
            IndiceNivelActual = 0;
            Niveles = new List<Func<Nivel>>(niveles);
            NivelActual = ConstruirNivel(IndiceNivelActual);
        }

        public void ReiniciarNivelActual()
        {
            NivelActual = ConstruirNivel(IndiceNivelActual);
        }

        public bool RegistrarNivelSuperado()
        {
            if(!NivelActual.Superado())
            {
                return false;
            }
            if (IndiceNivelActual < Niveles.Count - 1 && IndiceNivelActual == NivelMaximoDesbloqueado)
            {
                NivelMaximoDesbloqueado++;
                return true;
            }
            if (IndiceNivelActual == Niveles.Count - 1 && !EstaCompletada)
            {
                EstaCompletada = true;
                return true;
            }
            return false;
        }
        public bool EstaDesbloqueado(int indiceNivel)
        {
            if(indiceNivel < 0 || indiceNivel >= Niveles.Count)
            {
                return false;
            }
            return indiceNivel <= NivelMaximoDesbloqueado;
        }
        
        public bool SeleccionarNivel(int indiceNivel)
        {
            if (EstaDesbloqueado(indiceNivel))
            {
                IndiceNivelActual = indiceNivel;
                NivelActual = ConstruirNivel(IndiceNivelActual);
                return true;
            }
            return false;
        }

        private Nivel ConstruirNivel(int indiceNivel)
        {
            return Niveles[indiceNivel]();
        }
    }
}
