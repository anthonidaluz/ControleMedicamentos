using ControleDeMedicamentos.WebApp.ModuloRequisicoes;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorio;
    private readonly RepositorioRequisicaoSaidaEmArquivo repositorioRequisicaoSaida;

    public PacienteController(
        RepositorioPacienteEmArquivo repositorio,
        RepositorioRequisicaoSaidaEmArquivo repositorioRequisicaoSaida
    )
    {
        this.repositorio = repositorio;
        this.repositorioRequisicaoSaida = repositorioRequisicaoSaida;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorio.SelecionarTodos();

        List<ListarPacienteViewModel> viewModels = [];

        foreach (Paciente p in pacientes)
        {
            ListarPacienteViewModel vm = new ListarPacienteViewModel(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSus,
                p.Cpf
            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVm)
    {
        if (ExistePacienteComCartaoSus(cadastrarVm.CartaoSus))
            ModelState.AddModelError(nameof(cadastrarVm.CartaoSus), "Já existe um paciente cadastrado com o Cartão do SUS informado.");

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Paciente paciente = new Paciente(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.CartaoSus,
            cadastrarVm.Cpf
        );

        repositorio.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Paciente? pacienteSelecionado = repositorio.SelecionarPorId(id);

        if (pacienteSelecionado == null)
            return NotFound();

        EditarPacienteViewModel viewModel = new EditarPacienteViewModel(
            id,
            pacienteSelecionado.Nome,
            pacienteSelecionado.Telefone,
            pacienteSelecionado.CartaoSus,
            pacienteSelecionado.Cpf
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel editarVm)
    {
        if (ExistePacienteComCartaoSus(editarVm.CartaoSus, editarVm.Id))
            ModelState.AddModelError(nameof(editarVm.CartaoSus), "Já existe um paciente cadastrado com o Cartão do SUS informado.");

        if (!ModelState.IsValid)
            return View(editarVm);

        Paciente pacienteAtualizado = new Paciente(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.CartaoSus,
            editarVm.Cpf
        );

        bool conseguiuEditar = repositorio.Editar(editarVm.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Paciente? pacienteSelecionado = repositorio.SelecionarPorId(id);

        if (pacienteSelecionado == null)
            return NotFound();

        ExcluirPacienteViewModel viewModel = new ExcluirPacienteViewModel(
            id,
            pacienteSelecionado.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        if (ExistemRequisicoesDoPaciente(excluirVm.Id))
        {
            ModelState.AddModelError(string.Empty, "Não é possível excluir um paciente que possui requisições de saída.");

            return View(excluirVm);
        }

        bool conseguiuExcluir = repositorio.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private bool ExistePacienteComCartaoSus(string cartaoSus, int? idIgnorado = null)
    {
        foreach (Paciente p in repositorio.SelecionarTodos())
        {
            if (p.Id != idIgnorado && p.CartaoSus == cartaoSus)
                return true;
        }

        return false;
    }

    private bool ExistemRequisicoesDoPaciente(int idPaciente)
    {
        foreach (RequisicaoSaida r in repositorioRequisicaoSaida.SelecionarTodos())
        {
            if (r.Paciente?.Id == idPaciente)
                return true;
        }

        return false;
    }
}
