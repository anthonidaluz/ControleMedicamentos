using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public record FornecedorMedicamentoViewModel(int Id, string Nome);

public record ListarMedicamentoViewModel(
    int Id,
    string Nome,
    string Descricao,
    string NomeFornecedor,
    int QuantidadeEmEstoque
)
{
    public bool EmFalta => QuantidadeEmEstoque < 20;
}

public record CadastrarMedicamentoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(255, MinimumLength = 5,
        ErrorMessage = "O campo \"Descrição\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    int FornecedorId
)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
}

public record EditarMedicamentoViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(255, MinimumLength = 5,
        ErrorMessage = "O campo \"Descrição\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    int FornecedorId
)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
}

public record ExcluirMedicamentoViewModel(int Id, string Nome);
