using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>
{
    public RepositorioFornecedorEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contexto.Fornecedores;
    }
}