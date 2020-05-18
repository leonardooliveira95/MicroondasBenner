using Microondas.Entidades;
using Microondas.Repositorio.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Microondas.Repositorio.Repositorios
{
    public class RepositorioConfiguracaoArquivo : IRepositorioConfiguracao
    {
        private string caminho = System.AppDomain.CurrentDomain.BaseDirectory + @"\res\programas.json";

        public List<Programa> ConsultarProgramas()
        {
            string json = File.ReadAllText(caminho);
            List<Programa> programas = JsonConvert.DeserializeObject<List<Programa>>(json);

            return programas;
        }

        public void GravarPrograma(Programa programa)
        {
            string json = File.ReadAllText(caminho);
            List<Programa> programas = JsonConvert.DeserializeObject<List<Programa>>(json);

            int codigo = programas.Max(x => x.Codigo);
            programa.Codigo = codigo + 1;

            programas.Add(programa);

            File.WriteAllText(caminho, JsonConvert.SerializeObject(programas));
        }
    }
}
