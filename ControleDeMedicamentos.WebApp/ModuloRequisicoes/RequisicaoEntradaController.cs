using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntradaController : Controller
{
    private readonly RepositorioRequisicaoEntradaEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;

    public RequisicaoEntradaController(
        RepositorioRequisicaoEntradaEmArquivo repositorio,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioFuncionarioEmArquivo repositorioFuncionario
    )
    {
        this.repositorio = repositorio;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoEntradaViewModel> viewModels = [];

        foreach (RequisicaoEntrada r in repositorio.SelecionarTodos())
        {
            ListarRequisicaoEntradaViewModel vm = new ListarRequisicaoEntradaViewModel(
                r.Id,
                r.Medicamento?.Nome ?? "Não informado",
                r.Funcionario?.Nome ?? "Não informado",
                r.Quantidade,
                r.Data
            );

            viewModels.Add(vm);
        }

        viewModels.Reverse();

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarRequisicaoEntradaViewModel viewModel = new CadastrarRequisicaoEntradaViewModel(
            0,
            0,
            null,
            DateTime.Today
        ) with
        { Medicamentos = ObterMedicamentos(), Funcionarios = ObterFuncionarios() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel cadastrarVm)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(cadastrarVm.MedicamentoId);

        if (medicamento == null)
            ModelState.AddModelError(nameof(cadastrarVm.MedicamentoId), "O campo \"Medicamento\" deve ser preenchido.");

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(cadastrarVm.FuncionarioId);

        if (funcionario == null)
            ModelState.AddModelError(nameof(cadastrarVm.FuncionarioId), "O campo \"Funcionário\" deve ser preenchido.");

        if (!ModelState.IsValid || medicamento == null || funcionario == null)
        {
            return View(cadastrarVm with
            {
                Medicamentos = ObterMedicamentos(),
                Funcionarios = ObterFuncionarios()
            });
        }

        RequisicaoEntrada requisicao = new RequisicaoEntrada(
            medicamento,
            funcionario,
            cadastrarVm.Quantidade ?? 0,
            cadastrarVm.Data ?? DateTime.Today
        );

        repositorio.Cadastrar(requisicao);

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoRequisicaoEntradaViewModel> ObterMedicamentos()
    {
        List<OpcaoRequisicaoEntradaViewModel> viewModels = [];

        foreach (Medicamento m in repositorioMedicamento.SelecionarTodos())
            viewModels.Add(new OpcaoRequisicaoEntradaViewModel(m.Id, $"{m.Nome} (estoque: {m.QuantidadeEmEstoque})"));

        return viewModels;
    }

    private List<OpcaoRequisicaoEntradaViewModel> ObterFuncionarios()
    {
        List<OpcaoRequisicaoEntradaViewModel> viewModels = [];

        foreach (Funcionario f in repositorioFuncionario.SelecionarTodos())
            viewModels.Add(new OpcaoRequisicaoEntradaViewModel(f.Id, f.Nome));

        return viewModels;
    }
}
