using System;
using System.Collections.Generic;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios
{
    public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
    {
        public TelaFuncionario(RepositorioFuncionarioEmArquivo repositorio) : base("Funcionário", repositorio)
        {
        }

        public override void VisualizarTodos(bool deveExibirCabecalho)
        {
            if (deveExibirCabecalho)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Visualização de Funcionários");
                Console.WriteLine("---------------------------------");
            }

            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -15}",
                "Id", "Nome", "Telefone", "CPF"
            );

            List<Funcionario> registros = repositorio.SelecionarTodos();

            foreach (Funcionario f in registros)
            {
                Console.WriteLine(
                    "{0, -7} | {1, -30} | {2, -15} | {3, -15}",
                    f.Id, f.Nome, f.Telefone, f.Cpf
                );
            }

            if (deveExibirCabecalho)
            {
                Console.WriteLine("---------------------------------");
                Console.Write("Digite ENTER para continuar...");
                Console.ReadLine();
            }
        }

        protected override Funcionario ObterDadosCadastrais()
        {
            Console.Write("Digite o nome do funcionário: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o telefone do funcionário: ");
            string telefone = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o CPF do funcionário: ");
            string cpf = Console.ReadLine() ?? string.Empty;

            return new Funcionario(nome, telefone, cpf);
        }

        protected override bool ExisteRegistroComInformacoesExclusivas(Funcionario entidade, int? idIgnorado = null)
        {
            List<Funcionario> registros = repositorio.SelecionarTodos();

            foreach (Funcionario f in registros)
            {
                if (f.Id != idIgnorado && f.Cpf == entidade.Cpf)
                {
                    Console.WriteLine("---------------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Já existe um funcionário cadastrado com o CPF informado.");
                    Console.ResetColor();
                    Console.WriteLine("---------------------------------");
                    return true;
                }
            }

            return false;
        }
    }
}
