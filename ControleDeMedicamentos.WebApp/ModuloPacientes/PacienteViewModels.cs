using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public record ListarPacienteViewModel(int Id, string Nome, string Telefone, string CartaoSus, string Cpf);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$",
        ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.")]
    string Telefone,

    [Display(Name = "Cartão do SUS")]
    [Required(ErrorMessage = "O campo \"Cartão do SUS\" é obrigatório.")]
    [RegularExpression(@"^\d{15}$",
        ErrorMessage = "O campo \"Cartão do SUS\" deve conter exatamente 15 dígitos numéricos.")]
    string CartaoSus,

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{11}$",
        ErrorMessage = "O campo \"CPF\" deve conter exatamente 11 dígitos numéricos.")]
    string Cpf
);

public record EditarPacienteViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$",
        ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.")]
    string Telefone,

    [Display(Name = "Cartão do SUS")]
    [Required(ErrorMessage = "O campo \"Cartão do SUS\" é obrigatório.")]
    [RegularExpression(@"^\d{15}$",
        ErrorMessage = "O campo \"Cartão do SUS\" deve conter exatamente 15 dígitos numéricos.")]
    string CartaoSus,

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{11}$",
        ErrorMessage = "O campo \"CPF\" deve conter exatamente 11 dígitos numéricos.")]
    string Cpf
);

public record ExcluirPacienteViewModel(int Id, string Nome);
