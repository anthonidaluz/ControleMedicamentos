using System;
using System.Collections.Generic;
using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes
{
    public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>
    {
        public RepositorioRequisicaoSaidaEmArquivo(ContextoJson contexto) : base(contexto)
        {
        }
        protected override List<RequisicaoSaida> ObterRegistros()
        {
            return contexto.RequisicoesSaida;
        }
    }
}
