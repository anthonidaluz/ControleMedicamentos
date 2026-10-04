using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmArquivo repositorio;

    public FuncionarioController(RepositorioFuncionarioEmArquivo repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Funcionario> funcionarios = repositorio.SelecionarTodos();

        List<ListarFuncionarioViewModel> viewModels = [];

        foreach (Funcionario f in funcionarios)
        {
            ListarFuncionarioViewModel vm = new ListarFuncionarioViewModel(
                f.Id,
                f.Nome,
                f.Telefone,
                f.Cpf
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
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
    {
        if (ExisteFuncionarioComCpf(cadastrarVm.Cpf))
            ModelState.AddModelError(nameof(cadastrarVm.Cpf), "Já existe um funcionário cadastrado com o CPF informado.");

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Funcionario funcionario = new Funcionario(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cpf
        );

        repositorio.Cadastrar(funcionario);

        return RedirectToAction(nameof(Listar));
    }

    private bool ExisteFuncionarioComCpf(string cpf, int? idIgnorado = null)
    {
        foreach (Funcionario f in repositorio.SelecionarTodos())
        {
            if (f.Id != idIgnorado && f.Cpf == cpf)
                return true;
        }

        return false;
    }
}
