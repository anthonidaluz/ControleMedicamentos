using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class RepositorioRequisicaoEntradaEmArquivo : RepositorioBaseEmArquivo<RequisicaoEntrada>
{
    public RepositorioRequisicaoEntradaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoEntrada> ObterRegistros()
    {
        return contexto.RequisicoesEntrada;
    }
}