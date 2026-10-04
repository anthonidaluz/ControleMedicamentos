using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoSaidaController : Controller
{
    private readonly RepositorioRequisicaoSaidaEmArquivo repositorio;

    public RequisicaoSaidaController(RepositorioRequisicaoSaidaEmArquivo repositorio)
    {
        this.repositorio = repositorio;
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
}
