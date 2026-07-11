using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>
{
    public RepositorioMedicamentoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
}