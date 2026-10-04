using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public record OpcaoRequisicaoSaidaViewModel(int Id, string Nome);

public record ListarRequisicaoSaidaViewModel(
    int Id,
    string NomeMedicamento,
    string NomePaciente,
    int Quantidade,
    DateTime Data
);

public record CadastrarRequisicaoSaidaViewModel(
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Medicamento\" é obrigatório.")]
    int MedicamentoId,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Paciente\" é obrigatório.")]
    int PacienteId,

    [Required(ErrorMessage = "O campo \"Quantidade\" é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "A \"Quantidade\" deve ser maior que zero.")]
    int? Quantidade,

    [Required(ErrorMessage = "O campo \"Data\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? Data
)
{
    public List<OpcaoRequisicaoSaidaViewModel> Medicamentos { get; init; } = [];
    public List<OpcaoRequisicaoSaidaViewModel> Pacientes { get; init; } = [];
}
