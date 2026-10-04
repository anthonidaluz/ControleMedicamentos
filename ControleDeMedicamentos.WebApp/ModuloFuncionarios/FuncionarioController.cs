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
}
