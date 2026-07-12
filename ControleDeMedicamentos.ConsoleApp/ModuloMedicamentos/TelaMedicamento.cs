using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    public TelaMedicamento(
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioFornecedorEmArquivo repositorioFornecedor
    ) : base("Medicamento", repositorioMedicamento)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Medicamentos");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -15}",
            "Id", "Nome", "Fornecedor", "Descrição", "Estoque"
        );

        List<Medicamento> registros = repositorio.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            if (m.QuantidadeEmEstoque < 20)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -15}",
                    m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao, $"{m.QuantidadeEmEstoque} (Em falta)"
                );
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(
                    "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -15}",
                    m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao, m.QuantidadeEmEstoque
                );
            }
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do medicamento: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a descrição do medicamento: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");

        VisualizarFornecedores();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do fornecedor que deseja selecionar: ");
        int idFornecedor = Convert.ToInt32(Console.ReadLine());

        Fornecedor fornecedor = repositorioFornecedor.SelecionarPorId(idFornecedor)!;

        return new Medicamento(nome, descricao, fornecedor);
    }

    private void VisualizarFornecedores()
    {
        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        List<Fornecedor> registros = repositorioFornecedor.SelecionarTodos();

        foreach (Fornecedor f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cnpj
            );
        }
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Medicamento entidade, int? idIgnorado = null)
    {
        List<Medicamento> registros = repositorio.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            if (m.Id != idIgnorado && m.Nome.Equals(entidade.Nome, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um medicamento cadastrado com este nome.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                return true;
            }
        }

        return false;
    }
}
