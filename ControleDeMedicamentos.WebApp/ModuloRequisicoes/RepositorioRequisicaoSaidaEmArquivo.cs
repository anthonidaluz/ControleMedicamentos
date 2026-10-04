using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes
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
