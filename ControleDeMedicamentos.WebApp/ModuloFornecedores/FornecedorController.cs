using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public class FornecedorController : Controller
{
    private readonly RepositorioFornecedorEmArquivo repositorio;

    public FornecedorController(RepositorioFornecedorEmArquivo repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

        List<ListarFornecedorViewModel> viewModels = [];

        foreach (Fornecedor f in fornecedores)
        {
            ListarFornecedorViewModel vm = new ListarFornecedorViewModel(
                f.Id,
                f.Nome,
                f.Telefone,
                f.Cnpj
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
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVm)
    {
        if (ExisteFornecedorComCnpj(cadastrarVm.Cnpj))
            ModelState.AddModelError(nameof(cadastrarVm.Cnpj), "Já existe um fornecedor cadastrado com o CNPJ informado.");

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Fornecedor fornecedor = new Fornecedor(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cnpj
        );

        repositorio.Cadastrar(fornecedor);

        return RedirectToAction(nameof(Listar));
    }

    private bool ExisteFornecedorComCnpj(string cnpj, int? idIgnorado = null)
    {
        foreach (Fornecedor f in repositorio.SelecionarTodos())
        {
            if (f.Id != idIgnorado && f.Cnpj == cnpj)
                return true;
        }

        return false;
    }
}
