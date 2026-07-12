using System;
using System.Collections.Generic;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Modulos.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes
{
    public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaOpcoes, ITelaCrud
    {
        private readonly RepositorioPacienteEmArquivo repositorioPaciente;
        private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
        private readonly TelaPaciente telaPaciente;
        private readonly TelaMedicamento telaMedicamento;
        private RepositorioRequisicaoEntradaEmArquivo repositorioRequisicao;

        public TelaRequisicaoSaida(
            RepositorioRequisicaoSaidaEmArquivo repositorio,
            RepositorioPacienteEmArquivo repositorioPaciente,
            RepositorioMedicamentoEmArquivo repositorioMedicamento,
            TelaPaciente telaPaciente,
            TelaMedicamento telaMedicamento)
            : base("Requisição de Saída", repositorio)
        {
            this.repositorioPaciente = repositorioPaciente;
            this.repositorioMedicamento = repositorioMedicamento;
            this.telaPaciente = telaPaciente;
            this.telaMedicamento = telaMedicamento;
        }

        public override void VisualizarTodos(bool deveExibirCabecalho)
        {
            if (deveExibirCabecalho)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Visualização de Requisições de Saída");
                Console.WriteLine("----------------------------------------------------------------------");
            }

            Console.WriteLine(
                "{0, -5} | {1, -12} | {2, -20} | {3, -20} | {4, -10}",
                "Id", "Data", "Medicamento", "Paciente", "Qtd"
            );

            List<RequisicaoSaida> registros = repositorio.SelecionarTodos();

            foreach (RequisicaoSaida r in registros)
            {
                Console.WriteLine(
                    "{0, -5} | {1, -12:dd/MM/yyyy} | {2, -20} | {3, -20} | {4, -10}",
                    r.Id, r.Data, r.Medicamento.Nome, r.Paciente.Nome, r.Quantidade
                );
            }

            if (deveExibirCabecalho)
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.Write("Digite ENTER para continuar...");
                Console.ReadLine();
            }
        }

        protected override RequisicaoSaida ObterDadosCadastrais()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione o Medicamento:");
            Console.WriteLine("---------------------------------");
            telaMedicamento.VisualizarTodos(false);
            Console.Write("Digite o ID do Medicamento: ");
            int idMedicamento = Convert.ToInt32(Console.ReadLine());
            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(idMedicamento);

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione o Paciente:");
            Console.WriteLine("---------------------------------");
            telaPaciente.VisualizarTodos(false);
            Console.Write("Digite o ID do Paciente: ");
            int idPaciente = Convert.ToInt32(Console.ReadLine());
            Paciente pacienteSelecionado = repositorioPaciente.SelecionarPorId(idPaciente);

            int quantidade = 0;
            bool qtdValida = false;
            do
            {
                Console.Write($"Digite a quantidade a retirar (Estoque atual: {medicamentoSelecionado.QuantidadeEmEstoque}): ");
                qtdValida = int.TryParse(Console.ReadLine(), out quantidade);

                if (!qtdValida || quantidade <= 0)
                {
                    Console.WriteLine("[Erro] Digite um número válido maior que zero.");
                    qtdValida = false;
                }

                else if (quantidade > medicamentoSelecionado.QuantidadeEmEstoque)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Erro] Quantidade indisponível! Estoque atual é de apenas {medicamentoSelecionado.QuantidadeEmEstoque} unidades.");
                    Console.ResetColor();
                    qtdValida = false;
                }

            } while (!qtdValida);

            Console.Write("Digite a data (Ex: 12/07/2026): ");
            DateTime data;
            while (!DateTime.TryParse(Console.ReadLine(), out data))
            {
                Console.Write("[Erro] Data inválida. Digite novamente: ");
            }

            RequisicaoSaida novaRequisicao = new RequisicaoSaida(medicamentoSelecionado, pacienteSelecionado, quantidade, data);

            medicamentoSelecionado.RegistrarSaida(novaRequisicao);

            return novaRequisicao;
        }

        public new void Editar()
        {
            Console.WriteLine("A edição de Requisições não é permitida pelo sistema.");
            Console.ReadLine();
        }

        public new void Excluir()
        {
            Console.WriteLine("A exclusão de Requisições não é permitida pelo sistema.");
            Console.ReadLine();
        }
    }
}
