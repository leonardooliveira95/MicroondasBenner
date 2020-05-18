using Microondas.Repositorio.Interfaces;
using Microondas.Repositorio.Repositorios;

namespace Microondas.Repositorio.Fabrica
{
    public class FabricaRepositorioConfiguracao
    {
        public static IRepositorioConfiguracao Criar()
        {
            //TODO: Implementar criação de repositórios de teste
            return new RepositorioConfiguracaoArquivo();
        }
    }
}