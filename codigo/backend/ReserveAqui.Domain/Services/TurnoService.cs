using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Shared;
using ReserveAqui.Shared.Validacao;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepositorio turnoRepositorio;

        public TurnoService(ITurnoRepositorio turnoRepositorio)
        {
            this.turnoRepositorio = turnoRepositorio;
        }

        public TurnoDto ObterPorId(int id)
        {
            Validate.That(id).IsGreaterThan(0);

            return turnoRepositorio.ObterPorId(id).MapearTurno();
        }

        public List<TurnoDto> Listar()
        {
            return turnoRepositorio.Listar().MapearTurnos();
        }

        public List<TurnoDto> ListarPorIds(List<int> ids)
        {
            Validate.That(ids).IsNotNullOrEmpty();

            return turnoRepositorio.ListarPorIds(ids).MapearTurnos();
        }

        public void Inserir(TurnoDto turno)
        {
            Validate.That(turno).IsNotNull();
            Validate.That(turno.Descricao).IsNotNullOrWhiteSpace();
            Validate.That(turno.HorarioInicio).IsNotNull();
            Validate.That(turno.HorarioFim).IsNotNull();

            turnoRepositorio.Inserir(ConverterParaEntidade(turno));
        }

        public void Atualizar(TurnoDto turno)
        {
            Validate.That(turno).IsNotNull();
            Validate.That(turno.Descricao).IsNotNullOrWhiteSpace();
            Validate.That(turno.HorarioInicio).IsNotNull();
            Validate.That(turno.HorarioFim).IsNotNull();

            var turnoObtido = ObterPorId(turno.Id);
            Validate.That(turnoObtido).IsNotNull(Mensagens.ERRO_TURNO_NAO_ENCONTRADO);

            turnoRepositorio.Atualizar(ConverterParaEntidade(turno));
        }

        public void Deletar(int id)
        {
            var turnoObtido = ObterPorId(id);
            Validate.That(turnoObtido).IsNotNull(Mensagens.ERRO_TURNO_NAO_ENCONTRADO);

            turnoRepositorio.Deletar(id);
        }

        private Turno ConverterParaEntidade(TurnoDto turno)
        {
            return new Turno()
            {
                Id = turno.Id,
                Descricao = turno.Descricao,
                HorarioInicio = turno.HorarioInicio,
                HorarioFim = turno.HorarioFim
            };
        }
    }
}
