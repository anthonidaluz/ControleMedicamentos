using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public record ListarFuncionarioViewModel(int Id, string Nome, string Telefone, string Cpf);

public record CadastrarFuncionarioViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$",
        ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{11}$",
        ErrorMessage = "O campo \"CPF\" deve conter exatamente 11 dígitos numéricos.")]
    string Cpf
);

public record EditarFuncionarioViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$",
        ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{11}$",
        ErrorMessage = "O campo \"CPF\" deve conter exatamente 11 dígitos numéricos.")]
    string Cpf
);

public record ExcluirFuncionarioViewModel(int Id, string Nome);
