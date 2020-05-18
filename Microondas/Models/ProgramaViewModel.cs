using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microondas.Models
{
    public class ProgramaViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int TempoAquecimento { get; set; }
        public int Potencia { get; set; }
        public string CaractereAquecimento { get; set; }
        public string Instrucoes { get; set; }
    }
}