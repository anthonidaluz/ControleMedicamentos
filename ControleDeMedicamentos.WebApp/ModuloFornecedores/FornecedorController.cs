using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public class FornecedorController : Controller
{
    private readonly RepositorioFornecedorEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

    public FornecedorController(
        RepositorioFornecedorEmArquivo repositorio,
        RepositorioMedicamentoEmArquivo repositorioMedicamento
    )
    {
        this.repositorio = repositorio;
        this.repositorioMedicamento = repositorioMedicamento;
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

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Fornecedor? fornecedorSelecionado = repositorio.SelecionarPorId(id);

        if (fornecedorSelecionado == null)
            return NotFound();

        EditarFornecedorViewModel viewModel = new EditarFornecedorViewModel(
            id,
            fornecedorSelecionado.Nome,
            fornecedorSelecionado.Telefone,
            fornecedorSelecionado.Cnpj
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarFornecedorViewModel editarVm)
    {
        if (ExisteFornecedorComCnpj(editarVm.Cnpj, editarVm.Id))
            ModelState.AddModelError(nameof(editarVm.Cnpj), "Já existe um fornecedor cadastrado com o CNPJ informado.");

        if (!ModelState.IsValid)
            return View(editarVm);

        Fornecedor fornecedorAtualizado = new Fornecedor(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cnpj
        );

        bool conseguiuEditar = repositorio.Editar(editarVm.Id, fornecedorAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Fornecedor? fornecedorSelecionado = repositorio.SelecionarPorId(id);

        if (fornecedorSelecionado == null)
            return NotFound();

        ExcluirFornecedorViewModel viewModel = new ExcluirFornecedorViewModel(
            id,
            fornecedorSelecionado.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFornecedorViewModel excluirVm)
    {
        if (ExistemMedicamentosDoFornecedor(excluirVm.Id))
        {
            ModelState.AddModelError(string.Empty, "Não é possível excluir um fornecedor que possui medicamentos cadastrados.");

            return View(excluirVm);
        }

        bool conseguiuExcluir = repositorio.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

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

    private bool ExistemMedicamentosDoFornecedor(int idFornecedor)
    {
        foreach (Medicamento m in repositorioMedicamento.SelecionarTodos())
        {
            if (m.Fornecedor?.Id == idFornecedor)
                return true;
        }

        return false;
    }
}
