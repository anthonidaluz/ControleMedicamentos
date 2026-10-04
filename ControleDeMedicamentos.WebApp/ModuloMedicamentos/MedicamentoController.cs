using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmArquivo repositorio;
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    public MedicamentoController(
        RepositorioMedicamentoEmArquivo repositorio,
        RepositorioFornecedorEmArquivo repositorioFornecedor
    )
    {
        this.repositorio = repositorio;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Medicamento> medicamentos = repositorio.SelecionarTodos();

        List<ListarMedicamentoViewModel> viewModels = [];

        foreach (Medicamento m in medicamentos)
        {
            ListarMedicamentoViewModel vm = new ListarMedicamentoViewModel(
                m.Id,
                m.Nome,
                m.Descricao,
                m.Fornecedor?.Nome ?? "Não informado",
                m.QuantidadeEmEstoque
            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel viewModel = new CadastrarMedicamentoViewModel(
            string.Empty,
            string.Empty,
            0
        ) with
        { Fornecedores = ObterFornecedores() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(cadastrarVm.FornecedorId);

        if (fornecedor == null)
            ModelState.AddModelError(nameof(cadastrarVm.FornecedorId), "O campo \"Fornecedor\" deve ser preenchido.");

        if (ExisteMedicamentoComNome(cadastrarVm.Nome))
            ModelState.AddModelError(nameof(cadastrarVm.Nome), "Já existe um medicamento cadastrado com este nome.");

        if (!ModelState.IsValid || fornecedor == null)
            return View(cadastrarVm with { Fornecedores = ObterFornecedores() });

        Medicamento medicamento = new Medicamento(
            cadastrarVm.Nome,
            cadastrarVm.Descricao,
            fornecedor
        );

        repositorio.Cadastrar(medicamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Medicamento? medicamentoSelecionado = repositorio.SelecionarPorId(id);

        if (medicamentoSelecionado == null)
            return NotFound();

        EditarMedicamentoViewModel viewModel = new EditarMedicamentoViewModel(
            id,
            medicamentoSelecionado.Nome,
            medicamentoSelecionado.Descricao,
            medicamentoSelecionado.Fornecedor?.Id ?? 0
        ) with
        { Fornecedores = ObterFornecedores() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(editarVm.FornecedorId);

        if (fornecedor == null)
            ModelState.AddModelError(nameof(editarVm.FornecedorId), "O campo \"Fornecedor\" deve ser preenchido.");

        if (ExisteMedicamentoComNome(editarVm.Nome, editarVm.Id))
            ModelState.AddModelError(nameof(editarVm.Nome), "Já existe um medicamento cadastrado com este nome.");

        if (!ModelState.IsValid || fornecedor == null)
            return View(editarVm with { Fornecedores = ObterFornecedores() });

        Medicamento medicamentoAtualizado = new Medicamento(
            editarVm.Nome,
            editarVm.Descricao,
            fornecedor
        );

        bool conseguiuEditar = repositorio.Editar(editarVm.Id, medicamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private List<FornecedorMedicamentoViewModel> ObterFornecedores()
    {
        List<FornecedorMedicamentoViewModel> viewModels = [];

        foreach (Fornecedor f in repositorioFornecedor.SelecionarTodos())
            viewModels.Add(new FornecedorMedicamentoViewModel(f.Id, f.Nome));

        return viewModels;
    }

    private bool ExisteMedicamentoComNome(string nome, int? idIgnorado = null)
    {
        foreach (Medicamento m in repositorio.SelecionarTodos())
        {
            if (m.Id != idIgnorado && string.Equals(m.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}
