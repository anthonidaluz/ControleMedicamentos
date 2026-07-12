using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class Medicamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Fornecedor Fornecedor { get; set; } = null!;

    // Entradas
    public List<RequisicaoEntrada> Requisicoes { get; set; } = [];

    // Saídas
    public List<RequisicaoSaida> RequisicoesSaida { get; set; } = [];

    public Medicamento() { }

    public Medicamento(string nome, string descricao, Fornecedor fornecedor) : this()
    {
        Nome = nome;
        Descricao = descricao;
        Fornecedor = fornecedor;
    }

    public int QuantidadeEmEstoque
    {
        get
        {
            int totalEntradas = 0;
            foreach (RequisicaoEntrada req in Requisicoes)
                totalEntradas += req.Quantidade;

            int totalSaidas = 0;
            foreach (RequisicaoSaida req in RequisicoesSaida)
                totalSaidas += req.Quantidade;
            return totalEntradas - totalSaidas;
        }
    }

    public void RegistrarRequisicao(RequisicaoEntrada requisicao)
    {
        Requisicoes.Add(requisicao);
    }

    // Novo método para a Saída
    public void RegistrarSaida(RequisicaoSaida requisicao)
    {
        RequisicoesSaida.Add(requisicao);
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo \"Descrição\" deve conter entre 5 e 255 caracteres.");

        if (Fornecedor == null)
            erros.Add("O campo \"Fornecedor\" deve ser preenchido.");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Medicamento medicamentoAtualizado = (Medicamento)entidadeAtualizada;

        Nome = medicamentoAtualizado.Nome;
        Descricao = medicamentoAtualizado.Descricao;
        Fornecedor = medicamentoAtualizado.Fornecedor;
    }
}
