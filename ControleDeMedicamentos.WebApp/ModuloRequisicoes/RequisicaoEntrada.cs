using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntrada : EntidadeBase
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(Medicamento medicamento, int quantidade) : this()
    {
        Medicamento = medicamento;
        Quantidade = quantidade;

        medicamento.RegistrarRequisicao(this);
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoEntrada requisicaoAtualizada = (RequisicaoEntrada)entidadeAtualizada;

        Medicamento = requisicaoAtualizada.Medicamento;
        Quantidade = requisicaoAtualizada.Quantidade;
    }
}