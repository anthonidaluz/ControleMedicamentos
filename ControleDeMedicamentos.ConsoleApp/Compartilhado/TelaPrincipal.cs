using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private readonly TelaFornecedor telaFornecedor;
    private readonly TelaMedicamento telaMedicamento;
    private readonly TelaRequisicaoEntrada telaRequisicaoEntrada;

    public TelaPrincipal(ContextoJson contexto)
    {
        RepositorioFornecedorEmArquivo repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        RepositorioMedicamentoEmArquivo repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        RepositorioRequisicaoEntradaEmArquivo repositorioRequisicao = new RepositorioRequisicaoEntradaEmArquivo(contexto);

        telaFornecedor = new TelaFornecedor(repositorioFornecedor);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(repositorioRequisicao, repositorioMedicamento);
    }

    public ITelaOpcoes? ObterOpcaoMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Medicamentos");
        Console.WriteLine("3 - Gestão de Requisições de Entrada");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return telaFornecedor;

        if (opcaoMenuPrincipal == "2")
            return telaMedicamento;

        if (opcaoMenuPrincipal == "3")
            return telaRequisicaoEntrada;

        return null;
    }
}
