using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorio;

    public PacienteController(RepositorioPacienteEmArquivo repositorio)
    {
        this.repositorio = repositorio;
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

    private bool ExistePacienteComCartaoSus(string cartaoSus, int? idIgnorado = null)
    {
        foreach (Paciente p in repositorio.SelecionarTodos())
        {
            if (p.Id != idIgnorado && p.CartaoSus == cartaoSus)
                return true;
        }

        return false;
    }
}
