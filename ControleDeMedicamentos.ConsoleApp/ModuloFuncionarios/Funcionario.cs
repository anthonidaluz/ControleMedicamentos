using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;

    public Funcionario() { }

    public Funcionario(string nome, string telefone, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.");

        if (!Regex.IsMatch(Cpf, @"^\d{11}$"))
            erros.Add("O campo \"CPF\" deve conter exatamente 11 dígitos numéricos.");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Funcionario funcionarioAtualizado = (Funcionario)entidadeAtualizada;

        Nome = funcionarioAtualizado.Nome;
        Telefone = funcionarioAtualizado.Telefone;
        Cpf = funcionarioAtualizado.Cpf;
    }
}
