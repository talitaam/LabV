using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Shared.Validacao;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Services
{
    public class DiaFuncionamentoService : IDiaFuncionamentoService
    {
        private readonly IDiaFuncionamentoRepositorio diaFuncionamentoRepositorio;

        public DiaFuncionamentoService(IDiaFuncionamentoRepositorio diaFuncionamentoRepositorio)
        {
            this.diaFuncionamentoRepositorio = diaFuncionamentoRepositorio;
        }

        public List<DiaFuncionamentoDto> Listar()
        {
            return diaFuncionamentoRepositorio.ListarDiasFuncionamento().MapearDiasFuncionamento();
        }

        public bool ValidarFuncionamentoDia(int diaSemana)
        {
            var diaFuncionamento = diaFuncionamentoRepositorio.ObterDiaFuncionamento(diaSemana);

            Validate.That(diaFuncionamento).IsNotNull();

            return diaFuncionamento.Ativo;
        }

        public void AtualizarDiasFuncionamento(List<DiaFuncionamentoDto> diasFuncionamento)
        {
            Validate.That(diasFuncionamento).IsNotNullOrEmpty();

            foreach(var diaFuncionamento in diasFuncionamento)
            {
                diaFuncionamentoRepositorio.AtualizarDiaFuncionamento(diaFuncionamento.DiaSemana, diaFuncionamento.Ativo);
            }
        }
    }
}
