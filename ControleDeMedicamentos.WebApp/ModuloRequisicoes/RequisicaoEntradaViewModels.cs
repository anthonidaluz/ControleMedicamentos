using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public record OpcaoRequisicaoEntradaViewModel(int Id, string Nome);

public record ListarRequisicaoEntradaViewModel(
    int Id,
    string NomeMedicamento,
    string NomeFuncionario,
    int Quantidade,
    DateTime Data
);

public record CadastrarRequisicaoEntradaViewModel(
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    int MedicamentoId,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Funcionário\" deve ser preenchido.")]
    int FuncionarioId,

    [Required(ErrorMessage = "O campo \"Quantidade\" é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "A \"Quantidade\" deve ser maior que zero.")]
    int? Quantidade,

    [Required(ErrorMessage = "O campo \"Data\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? Data
)
{
    public List<OpcaoRequisicaoEntradaViewModel> Medicamentos { get; init; } = [];
    public List<OpcaoRequisicaoEntradaViewModel> Funcionarios { get; init; } = [];
}
