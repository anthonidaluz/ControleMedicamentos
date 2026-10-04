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
}
