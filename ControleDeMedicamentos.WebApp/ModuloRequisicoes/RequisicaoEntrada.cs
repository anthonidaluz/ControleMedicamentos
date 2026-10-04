using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntrada : EntidadeBase
{
    public Medicamento Medicamento { get; set; } = null!;
    public Funcionario Funcionario { get; set; } = null!;
    public int Quantidade { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(Medicamento medicamento, Funcionario funcionario, int quantidade, DateTime data) : this()
    {
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
        Data = data;

        medicamento.RegistrarRequisicao(this);
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        if (Funcionario == null)
            erros.Add("O campo \"Funcionário\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        if (Data == DateTime.MinValue)
            erros.Add("A \"Data\" informada é inválida.");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoEntrada requisicaoAtualizada = (RequisicaoEntrada)entidadeAtualizada;

        Medicamento = requisicaoAtualizada.Medicamento;
        Funcionario = requisicaoAtualizada.Funcionario;
        Quantidade = requisicaoAtualizada.Quantidade;
        Data = requisicaoAtualizada.Data;
    }
}
