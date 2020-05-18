using Microondas.Entidades;
using Microondas.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Microondas.Servico
{
    public class ConfigurarMicroondasServico
    {
        private readonly IRepositorioConfiguracao repositorioConfiguracao;
        private static Timer _timer;

        public ConfigurarMicroondasServico(IRepositorioConfiguracao repositorioConfiguracao)
        {
            this.repositorioConfiguracao = repositorioConfiguracao;
        }

        public List<Programa> ConsultarProgramas()
        {
            return repositorioConfiguracao.ConsultarProgramas();
        }

        public void GravarPrograma(Programa programa)
        {
            repositorioConfiguracao.GravarPrograma(programa);
        }

        public void IniciarMicroondas(string alimento, int tempo, int potencia, string caractere, Func<string, bool, string> callback)
        {
            if (string.IsNullOrEmpty(alimento))
                throw new ArgumentException("Alimento não preenchido");

            if (tempo < 1 || tempo > (60 * 2))
                throw new ArgumentException("Tempo deve ser entre 1s e 2m");

            if (potencia < 1 || potencia > 10)
                throw new ArgumentException("Potência deve ser entre 1 e 10");

            int execucoes = 0;

            //Executa o timer uma vez imediatamente
            _timer = new Timer(_ =>
            {
                if (execucoes < tempo)
                {
                    alimento += new string(!string.IsNullOrEmpty(caractere)  ? caractere[0] : '.', potencia);
                    callback(alimento, false);

                    //Reconfigura o timer para executar daqui 1s
                    _timer.Change(1000, Timeout.Infinite);

                    execucoes++;
                }
                else
                {
                    callback(null, true);
                }
                
            }, null, 0, Timeout.Infinite);
        }

        public void PararMicroondas()
        {
            if(_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
