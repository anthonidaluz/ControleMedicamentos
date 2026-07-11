using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

    public TelaRequisicaoEntrada(
        RepositorioRequisicaoEntradaEmArquivo repositorioRequisicao,
        RepositorioMedicamentoEmArquivo repositorioMedicamento
    ) : base("Requisição de Entrada", repositorioRequisicao)
    {
        this.repositorioMedicamento = repositorioMedicamento;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Requisições de Entrada");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10} | {3, -15}",
            "Id", "Medicamento", "Qtd", "Data"
        );

        List<RequisicaoEntrada> registros = repositorio.SelecionarTodos();

        foreach (RequisicaoEntrada r in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10} | {3, -15}",
                r.Id, r.Medicamento.Nome, r.Quantidade, r.Data.ToShortDateString()
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override RequisicaoEntrada ObterDadosCadastrais()
    {
        VisualizarMedicamentos();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do medicamento que deseja requisitar: ");
        int idMedicamento = Convert.ToInt32(Console.ReadLine());

        Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

        Console.Write("Digite a quantidade que deseja requisitar: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        return new RequisicaoEntrada(medicamento, quantidade);
    }

    private void VisualizarMedicamentos()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
            "Id", "Nome", "Fornecedor", "Descrição"
        );

        List<Medicamento> registros = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao
            );
        }
    }

    protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        return false;
    }
}