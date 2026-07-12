using System;
using System.Collections.Generic;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Modulos.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes
{
    public class RequisicaoSaida : EntidadeBase
    {
        public Medicamento Medicamento { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }

        public RequisicaoSaida() { }

        public RequisicaoSaida(Medicamento medicamento, Paciente paciente, int quantidade, DateTime data) : this()
        {
            Medicamento = medicamento;
            Paciente = paciente;
            Quantidade = quantidade;
            Data = data;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (Medicamento == null)
                erros.Add("O campo \"Medicamento\" é obrigatório.");

            if (Paciente == null)
                erros.Add("O campo \"Paciente\" é obrigatório.");

            if (Quantidade <= 0)
                erros.Add("A \"Quantidade\" deve ser maior que zero.");

            if (Data == DateTime.MinValue)
                erros.Add("A \"Data\" informada é inválida.");

            return erros;
        }

        public override void Atualizar(EntidadeBase entidadeAtualizada)
        {
            RequisicaoSaida requisicaoAtualizada = (RequisicaoSaida)entidadeAtualizada;

            Medicamento = requisicaoAtualizada.Medicamento;
            Paciente = requisicaoAtualizada.Paciente;
            Quantidade = requisicaoAtualizada.Quantidade;
            Data = requisicaoAtualizada.Data;
        }
    }
}
