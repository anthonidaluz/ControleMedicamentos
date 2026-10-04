using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntradaController : Controller
{
    private readonly RepositorioRequisicaoEntradaEmArquivo repositorio;

    public RequisicaoEntradaController(RepositorioRequisicaoEntradaEmArquivo repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoEntradaViewModel> viewModels = [];

        foreach (RequisicaoEntrada r in repositorio.SelecionarTodos())
        {
            ListarRequisicaoEntradaViewModel vm = new ListarRequisicaoEntradaViewModel(
                r.Id,
                r.Medicamento?.Nome ?? "Não informado",
                r.Funcionario?.Nome ?? "Não informado",
                r.Quantidade,
                r.Data
            );

            viewModels.Add(vm);
        }

        viewModels.Reverse();

        return View(viewModels);
    }
}
