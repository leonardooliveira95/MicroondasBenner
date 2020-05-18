using System;

namespace Microondas.Entidades
{
    public class Programa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int TempoAquecimento { get; set; }
        public int Potencia { get; set; }
        public string CaractereAquecimento { get; set; }
        public string Instrucoes { get; set; }
    }
}
