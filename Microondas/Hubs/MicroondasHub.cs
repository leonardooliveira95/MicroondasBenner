using System.Threading.Tasks;
using Microondas.Repositorio.Fabrica;
using Microondas.Servico;
using Microsoft.AspNet.SignalR;

namespace Microondas.Hubs
{
    public class MicroondasHub : Hub
    {
        private ConfigurarMicroondasServico configurarMicroondasServico;

        public MicroondasHub()
        {
            configurarMicroondasServico = new ConfigurarMicroondasServico(FabricaRepositorioConfiguracao.Criar());
        }

        //Singleton de instância do Hub
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroondasHub>();

        public IHubContext GetContext()
        {
            return hubContext;
        }

        public string ExibirProgresso(string texto, bool terminou)
        {
            if(!terminou)
                hubContext.Clients.All.exibirProgressoAquecimento(texto, terminou);
            else
                hubContext.Clients.All.exibirProgressoAquecimento("Aquecimento terminado", terminou);

            return "";
        }

        public void LigarMicroondas(string alimento, int tempo, int potencia, string caractere)
        {
            configurarMicroondasServico.IniciarMicroondas(alimento, tempo, potencia, caractere, ExibirProgresso);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            configurarMicroondasServico.PararMicroondas();

            return base.OnDisconnected(stopCalled);
        }
    }
}