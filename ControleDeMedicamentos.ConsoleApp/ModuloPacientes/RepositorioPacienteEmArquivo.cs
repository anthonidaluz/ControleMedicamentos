using System;
using System.Collections.Generic;
using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.Modulos.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes
{
    public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>
    {
        public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto)
        {
        }

        protected override List<Paciente> ObterRegistros()
        {
            return contexto.Pacientes;
        }
    }
}
