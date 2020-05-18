using Microondas.Entidades;
using System;
using System.Collections.Generic;

namespace Microondas.Repositorio.Interfaces
{
    public interface IRepositorioConfiguracao
    {
        List<Programa> ConsultarProgramas();
        void GravarPrograma(Programa programa);
    }
}
