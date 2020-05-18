using Microondas.Entidades;
using Microondas.Models;
using Microondas.Repositorio.Fabrica;
using Microondas.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Microondas.Controllers
{
    public class MicroondasController : Controller
    {
        private ConfigurarMicroondasServico configurarMicroondasServico;

        public MicroondasController()
        {
            configurarMicroondasServico = new ConfigurarMicroondasServico(FabricaRepositorioConfiguracao.Criar());
        }

        public ActionResult BuscarProgramas()
        {
            List<Programa> lista = configurarMicroondasServico.ConsultarProgramas();
            List<ProgramaViewModel> resultado = new List<ProgramaViewModel>();

            foreach(Programa p in lista)
            {
                resultado.Add(new ProgramaViewModel
                {
                    Codigo = p.Codigo,
                    CaractereAquecimento = p.CaractereAquecimento,
                    Instrucoes = p.Instrucoes,
                    Nome = p.Nome,
                    Potencia = p.Potencia,
                    TempoAquecimento = p.TempoAquecimento
                });
            }

            return PartialView("_BuscarProgramas",resultado);
        }

        public ActionResult GravarPrograma(ProgramaViewModel programa)
        {
            configurarMicroondasServico.GravarPrograma(new Programa
            {
                CaractereAquecimento = programa.CaractereAquecimento,
                Instrucoes = programa.Instrucoes,
                Nome = programa.Nome,
                Potencia = programa.Potencia,
                TempoAquecimento = programa.TempoAquecimento

            });

            return Json(new { mensagem = "Programa gravado" });
        }
    }
}