using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoSaidaController : Controller
{
    private readonly RepositorioRequisicaoSaidaEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;

    public RequisicaoSaidaController(
        RepositorioRequisicaoSaidaEmArquivo repositorio,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioPacienteEmArquivo repositorioPaciente
    )
    {
        this.repositorio = repositorio;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoSaidaViewModel> viewModels = [];

        foreach (RequisicaoSaida r in repositorio.SelecionarTodos())
        {
            ListarRequisicaoSaidaViewModel vm = new ListarRequisicaoSaidaViewModel(
                r.Id,
                r.Medicamento?.Nome ?? "Não informado",
                r.Paciente?.Nome ?? "Não informado",
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
        CadastrarRequisicaoSaidaViewModel viewModel = new CadastrarRequisicaoSaidaViewModel(
            0,
            0,
            null,
            DateTime.Today
        ) with
        { Medicamentos = ObterMedicamentos(), Pacientes = ObterPacientes() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel cadastrarVm)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(cadastrarVm.MedicamentoId);

        if (medicamento == null)
            ModelState.AddModelError(nameof(cadastrarVm.MedicamentoId), "O campo \"Medicamento\" é obrigatório.");

        Paciente? paciente = repositorioPaciente.SelecionarPorId(cadastrarVm.PacienteId);

        if (paciente == null)
            ModelState.AddModelError(nameof(cadastrarVm.PacienteId), "O campo \"Paciente\" é obrigatório.");

        if (medicamento != null && cadastrarVm.Quantidade > medicamento.QuantidadeEmEstoque)
        {
            ModelState.AddModelError(
                nameof(cadastrarVm.Quantidade),
                $"Quantidade indisponível! O estoque atual é de apenas {medicamento.QuantidadeEmEstoque} unidades."
            );
        }

        if (!ModelState.IsValid || medicamento == null || paciente == null)
        {
            return View(cadastrarVm with
            {
                Medicamentos = ObterMedicamentos(),
                Pacientes = ObterPacientes()
            });
        }

        RequisicaoSaida requisicao = new RequisicaoSaida(
            medicamento,
            paciente,
            cadastrarVm.Quantidade ?? 0,
            cadastrarVm.Data ?? DateTime.Today
        );

        medicamento.RegistrarSaida(requisicao);

        repositorio.Cadastrar(requisicao);

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoRequisicaoSaidaViewModel> ObterMedicamentos()
    {
        List<OpcaoRequisicaoSaidaViewModel> viewModels = [];

        foreach (Medicamento m in repositorioMedicamento.SelecionarTodos())
            viewModels.Add(new OpcaoRequisicaoSaidaViewModel(m.Id, $"{m.Nome} (estoque: {m.QuantidadeEmEstoque})"));

        return viewModels;
    }

    private List<OpcaoRequisicaoSaidaViewModel> ObterPacientes()
    {
        List<OpcaoRequisicaoSaidaViewModel> viewModels = [];

        foreach (Paciente p in repositorioPaciente.SelecionarTodos())
            viewModels.Add(new OpcaoRequisicaoSaidaViewModel(p.Id, p.Nome));

        return viewModels;
    }
}
