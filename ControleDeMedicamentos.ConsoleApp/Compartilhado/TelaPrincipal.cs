using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;
using ControleDeMedicamentos.ConsoleApp.Modulos.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private readonly TelaFornecedor telaFornecedor;
    private readonly TelaMedicamento telaMedicamento;
    private readonly TelaPaciente telaPaciente;
    private readonly TelaFuncionario telaFuncionario;
    private readonly TelaRequisicaoEntrada telaRequisicaoEntrada;

    public TelaPrincipal(ContextoJson contexto)
    {
        RepositorioFornecedorEmArquivo repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        RepositorioMedicamentoEmArquivo repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        RepositorioPacienteEmArquivo repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        RepositorioFuncionarioEmArquivo repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
        RepositorioRequisicaoEntradaEmArquivo repositorioRequisicao = new RepositorioRequisicaoEntradaEmArquivo(contexto);

        telaFornecedor = new TelaFornecedor(repositorioFornecedor);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaPaciente = new TelaPaciente(repositorioPaciente);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(repositorioRequisicao, repositorioMedicamento);
    }

    public ITelaOpcoes? ObterOpcaoMenuPrincipal()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Medicamentos");
        Console.WriteLine("3 - Gestão de Pacientes");
        Console.WriteLine("4 - Gestão de Funcionarios");
        Console.WriteLine("5 - Gestão de Requisições de Entrada");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();
        Console.ResetColor();

        if (opcaoMenuPrincipal == "1")
            return telaFornecedor;

        if (opcaoMenuPrincipal == "2")
            return telaMedicamento;

        if (opcaoMenuPrincipal == "3")
            return telaPaciente;

        if (opcaoMenuPrincipal == "4")
            return telaRequisicaoEntrada;



        return null;
    }
}
