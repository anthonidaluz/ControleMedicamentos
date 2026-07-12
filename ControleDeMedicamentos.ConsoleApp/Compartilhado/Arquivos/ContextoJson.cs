using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;
using ControleDeMedicamentos.ConsoleApp.Modulos.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

public class ContextoJson
{
    private readonly string caminhoArquivoDados;

    public List<Fornecedor> Fornecedores { get; set; } = [];
    public List<Paciente> Pacientes { get; set; } = [];
    public List<Funcionario> Funcionarios { get; set; } = [];
    public List<Medicamento> Medicamentos { get; set; } = [];
    public List<RequisicaoEntrada> RequisicoesEntrada { get; set; } = [];
    public List<RequisicaoSaida> RequisicoesSaida { get; set; } = [];

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorioAplicativo = Path.Join(caminhoAppData, "ControleDeMedicamentos-Backend");

        Directory.CreateDirectory(caminhoDiretorioAplicativo);

        caminhoArquivoDados = Path.Join(caminhoDiretorioAplicativo, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, options);

        File.WriteAllText(caminhoArquivoDados, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
            return;

        string jsonString = File.ReadAllText(caminhoArquivoDados);

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo =
            JsonSerializer.Deserialize<ContextoJson>(jsonString, options);

        if (contextoSalvo == null)
            return;

        Fornecedores = contextoSalvo.Fornecedores;
        Pacientes = contextoSalvo.Pacientes;
        Funcionarios = contextoSalvo.Funcionarios;
        Medicamentos = contextoSalvo.Medicamentos;
        RequisicoesEntrada = contextoSalvo.RequisicoesEntrada;
        RequisicoesSaida = contextoSalvo.RequisicoesSaida;
    }
}
