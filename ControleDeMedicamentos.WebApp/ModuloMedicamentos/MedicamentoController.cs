using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmArquivo repositorio;

    public MedicamentoController(RepositorioMedicamentoEmArquivo repositorio)
    {
        this.repositorio = repositorio;
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
}
